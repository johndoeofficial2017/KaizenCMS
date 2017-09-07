using Kendo.Mvc;
namespace UIServer
{
    public static class Help
    {
        internal static string ApplyFilter(IFilterDescriptor filter)
        {
            var filters = string.Empty;
            if (filter is CompositeFilterDescriptor)
            {
                filters += "(";
                var compositeFilterDescriptor = (CompositeFilterDescriptor)filter;
                foreach (IFilterDescriptor childFilter in compositeFilterDescriptor.FilterDescriptors)
                {
                    filters += ApplyFilter(childFilter);
                    filters += " " + compositeFilterDescriptor.LogicalOperator.ToString() + " ";
                }
            }
            else
            {
                string filterDescriptor = "{0} {1}{2}";
                var descriptor = (FilterDescriptor)filter;
                if (descriptor.Operator == FilterOperator.StartsWith)
                {
                    filterDescriptor = string.Format(filterDescriptor, descriptor.Member, "LIKE", "'" + descriptor.Value + "%'");
                }
                else if (descriptor.Operator == FilterOperator.EndsWith)
                {
                    filterDescriptor = string.Format(filterDescriptor, descriptor.Member, "LIKE", "'%" + descriptor.Value + "'");
                }
                else if (descriptor.Operator == FilterOperator.Contains)
                {
                    filterDescriptor = string.Format(filterDescriptor, descriptor.Member, "LIKE", "'%" + descriptor.Value + "%'");
                }
                else if (descriptor.Operator == FilterOperator.DoesNotContain)
                {
                    filterDescriptor = string.Format(filterDescriptor, descriptor.Member, "NOT LIKE", "'%" + descriptor.Value + "%'");
                }
                else if (descriptor.Operator == FilterOperator.IsEqualTo)
                {
                    filterDescriptor = string.Format(filterDescriptor, descriptor.Member, "=\'", descriptor.Value + "\'");
                }
                else if (descriptor.Operator == FilterOperator.IsNotEqualTo)
                {
                    filterDescriptor = string.Format(filterDescriptor, descriptor.Member, "<>", "'" + descriptor.Value.ToString().Trim() + "'");
                }
                else if (descriptor.Operator == FilterOperator.IsGreaterThan)
                {
                    filterDescriptor = string.Format(filterDescriptor, descriptor.Member, ">", "'" + descriptor.Value + "'");
                }
                else if (descriptor.Operator == FilterOperator.IsGreaterThanOrEqualTo)
                {
                    filterDescriptor = string.Format(filterDescriptor, descriptor.Member, ">=", "'" + descriptor.Value + "'");
                }
                else if (descriptor.Operator == FilterOperator.IsLessThan)
                {
                    filterDescriptor = string.Format(filterDescriptor, descriptor.Member, "<", "'" + descriptor.Value + "'");
                }
                else if (descriptor.Operator == FilterOperator.IsLessThanOrEqualTo)
                {
                    filterDescriptor = string.Format(filterDescriptor, descriptor.Member, "<=", "'" + descriptor.Value + "'");
                }

                filters = filterDescriptor;
            }

            filters = filters.EndsWith("And ") == true ? filters.Substring(0, filters.Length - 4) + ")" : filters;
            filters = filters.EndsWith("Or ") == true ? filters.Substring(0, filters.Length - 4) + ")" : filters;

            return filters;
        }
        internal static string ApplyFilter(string FieldID, int FltrOperator, string Searchcritaria)
        {
            string filterDescriptor = "{0} {1} {2}";
            switch (FltrOperator)
            {
                case 0://IsLessThan
                    filterDescriptor = string.Format(filterDescriptor, FieldID, "<", "'" + Searchcritaria.Trim() + "'");
                    break;
                case 1://Is Less Than Or Equal To
                    filterDescriptor = string.Format(filterDescriptor, FieldID, "<=", "'" + Searchcritaria.Trim() + "'");
                    break;
                case 2: //IsEqualTo
                    filterDescriptor = string.Format(filterDescriptor, FieldID, "=\'", Searchcritaria.Trim() + "\'");
                    break;
                case 3://Is Not Equal To
                    filterDescriptor = string.Format(filterDescriptor, FieldID, "<>", "'" + Searchcritaria.Trim() + "'");
                    break;
                case 4: //Is Greater Than Or Equal To
                    filterDescriptor = string.Format(filterDescriptor, FieldID, ">=", "'" + Searchcritaria.Trim() + "'");
                    break;
                case 5:
                    filterDescriptor = string.Format(filterDescriptor, FieldID, ">", "'" + Searchcritaria.Trim() + "'");
                    break;
                case 6://StartsWith
                    filterDescriptor = string.Format(filterDescriptor, FieldID, "LIKE", "'" + Searchcritaria.Trim() + "%'");
                    break;
                case 7: //Ends With
                    filterDescriptor = string.Format(filterDescriptor, FieldID, "LIKE", "'%" + Searchcritaria.Trim() + "'");
                    break;
                case 8: //Contains
                    filterDescriptor = string.Format(filterDescriptor, FieldID, "LIKE", "'%" + Searchcritaria.Trim() + "%'");
                    break;
                case 9: //NOT Contained In
                    filterDescriptor = string.Format(filterDescriptor, FieldID, "NOT LIKE", "'%" + Searchcritaria.Trim() + "%'");
                    break;
            }
            return filterDescriptor;
        }

    }
}