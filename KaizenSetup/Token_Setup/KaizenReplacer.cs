using System;
using System.Collections.Generic;
using System.Text;

namespace KaizenSetup.Token_Setup
{
    public class KaizenReplacer
    {
        private readonly string TokenOpen;
        private readonly string TokenClose;

        public KaizenReplacer(string tokenOpen, string tokenClose, bool caseSensitive = true)
        {
            if (string.IsNullOrEmpty(tokenOpen) || string.IsNullOrEmpty(tokenClose))
                throw new ArgumentException("Token must have opening and closing delimiters, such as \"{\" and \"}\".");

            TokenOpen = tokenOpen;
            TokenClose = tokenClose;

            var stringComparer = caseSensitive ? StringComparer.Ordinal : StringComparer.InvariantCultureIgnoreCase;
            OccurrencesOfToken = new Dictionary<string, List<TokenOccurrence>>(stringComparer);
        }

        private readonly KaizenReplacerSnippet RootSnippet = new KaizenReplacerSnippet("");

        private class TokenOccurrence
        {
            public KaizenReplacerSnippet Snippet;
            public int Start; // Position of a token in the snippet.
            public int End; // Position of a token in the snippet.
        }

        private readonly Dictionary<string, List<TokenOccurrence>> OccurrencesOfToken;

        public void Append(string text)
        {
            var snippet = new KaizenReplacerSnippet(text);
            RootSnippet.Append(snippet);
            ExtractTokens(snippet);
        }

        public bool Replace(string token, string text)
        {
            ValidateToken(token, text, false);
            List<TokenOccurrence> occurrences;
            if (OccurrencesOfToken.TryGetValue(token, out occurrences) && occurrences.Count > 0)
            {
                OccurrencesOfToken.Remove(token);
                var snippet = new KaizenReplacerSnippet(text);
                foreach (var occurrence in occurrences)
                    occurrence.Snippet.Replace(occurrence.Start, occurrence.End, snippet);
                ExtractTokens(snippet);
                return true;
            }
            return false;
        }

        public bool InsertBefore(string token, string text)
        {
            ValidateToken(token, text, false);
            List<TokenOccurrence> occurrences;
            if (OccurrencesOfToken.TryGetValue(token, out occurrences) && occurrences.Count > 0)
            {
                var snippet = new KaizenReplacerSnippet(text);
                foreach (var occurrence in occurrences)
                    occurrence.Snippet.InsertBefore(occurrence.Start, snippet);
                ExtractTokens(snippet);
                return true;
            }
            return false;
        }

        public bool InsertAfter(string token, string text)
        {
            ValidateToken(token, text, false);
            List<TokenOccurrence> occurrences;
            if (OccurrencesOfToken.TryGetValue(token, out occurrences) && occurrences.Count > 0)
            {
                var snippet = new KaizenReplacerSnippet(text);
                foreach (var occurrence in occurrences)
                    occurrence.Snippet.InsertAfter(occurrence.End, snippet);
                ExtractTokens(snippet);
                return true;
            }
            return false;
        }

        public bool Contains(string token)
        {
            ValidateToken(token, token, false);
            List<TokenOccurrence> occurrences;
            if (OccurrencesOfToken.TryGetValue(token, out occurrences))
                return occurrences.Count > 0;
            return false;
        }

        private void ExtractTokens(KaizenReplacerSnippet snippet)
        {
            int last = 0;
            while (last < snippet.Text.Length)
            {
                int start = snippet.Text.IndexOf(TokenOpen, last, StringComparison.InvariantCultureIgnoreCase);
                if (start == -1)
                    return;
                int end = snippet.Text.IndexOf(TokenClose, start + TokenOpen.Length, StringComparison.InvariantCultureIgnoreCase);
                if (end == -1)
                    throw new ArgumentException(string.Format("Token is opened but not closed in text \"{0}\".", snippet.Text));
                int eol = snippet.Text.IndexOf('\n', start + TokenOpen.Length);
                if (eol != -1 && eol < end)
                {
                    last = eol + 1;
                    continue;
                }

                end += TokenClose.Length;
                string token = snippet.Text.Substring(start, end - start);
                string context = snippet.Text;
                ValidateToken(token, context, true);

                var tokenOccurrence = new TokenOccurrence { Snippet = snippet, Start = start, End = end };
                List<TokenOccurrence> occurrences;
                if (OccurrencesOfToken.TryGetValue(token, out occurrences))
                    occurrences.Add(tokenOccurrence);
                else
                    OccurrencesOfToken.Add(token, new List<TokenOccurrence> { tokenOccurrence });

                last = end;
            }
        }

        private void ValidateToken(string token, string context, bool alreadyValidatedStartAndEnd)
        {
            if (!alreadyValidatedStartAndEnd)
            {
                if (!token.StartsWith(TokenOpen, StringComparison.InvariantCultureIgnoreCase))
                    throw new ArgumentException(string.Format("Token \"{0}\" shoud start with \"{1}\". Used with text \"{2}\".", token, TokenOpen, context));
                int closePosition = token.IndexOf(TokenClose, StringComparison.InvariantCultureIgnoreCase);
                if (closePosition == -1)
                    throw new ArgumentException(string.Format("Token \"{0}\" should end with \"{1}\". Used with text \"{2}\".", token, TokenClose, context));
                if (closePosition != token.Length - TokenClose.Length)
                    throw new ArgumentException(string.Format("Token \"{0}\" is closed before the end of the token. Used with text \"{1}\".", token, context));
            }

            if (token.Length == TokenOpen.Length + TokenClose.Length)
                throw new ArgumentException(string.Format("Token has no body. Used with text \"{0}\".", context));
            if (token.Contains("\n"))
                throw new ArgumentException(string.Format("Unexpected end-of-line within a token. Used with text \"{0}\".", context));
            if (token.IndexOf(TokenOpen, TokenOpen.Length, StringComparison.InvariantCultureIgnoreCase) != -1)
                throw new ArgumentException(string.Format("Next token is opened before a previous token was closed in token \"{0}\". Used with text \"{1}\".", token, context));
        }

        public override string ToString()
        {
            int totalTextLength = RootSnippet.GetLength();
            var sb = new StringBuilder(totalTextLength);
            RootSnippet.ToString(sb);
            if (sb.Length != totalTextLength)
                throw new InvalidOperationException(string.Format(
                    "Internal error: Calculated total text length ({0}) is different from actual ({1}).",
                    totalTextLength, sb.Length));
            return sb.ToString();
        }

    }
}
