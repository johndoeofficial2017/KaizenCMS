USE [master]

/****** Object:  Database [Kaizen]    Script Date: 4/25/2016 8:21:05 PM ******/
CREATE DATABASE [Kaizen]


ALTER DATABASE [Kaizen] SET COMPATIBILITY_LEVEL = 120

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Kaizen].[dbo].[sp_fulltext_database] @action = 'enable'
end


ALTER DATABASE [Kaizen] SET ANSI_NULL_DEFAULT OFF 


ALTER DATABASE [Kaizen] SET ANSI_NULLS OFF 


ALTER DATABASE [Kaizen] SET ANSI_PADDING OFF 


ALTER DATABASE [Kaizen] SET ANSI_WARNINGS OFF 


ALTER DATABASE [Kaizen] SET ARITHABORT OFF 


ALTER DATABASE [Kaizen] SET AUTO_CLOSE OFF 


ALTER DATABASE [Kaizen] SET AUTO_SHRINK OFF 


ALTER DATABASE [Kaizen] SET AUTO_UPDATE_STATISTICS ON 


ALTER DATABASE [Kaizen] SET CURSOR_CLOSE_ON_COMMIT OFF 


ALTER DATABASE [Kaizen] SET CURSOR_DEFAULT  GLOBAL 


ALTER DATABASE [Kaizen] SET CONCAT_NULL_YIELDS_NULL OFF 


ALTER DATABASE [Kaizen] SET NUMERIC_ROUNDABORT OFF 


ALTER DATABASE [Kaizen] SET QUOTED_IDENTIFIER OFF 


ALTER DATABASE [Kaizen] SET RECURSIVE_TRIGGERS OFF 


ALTER DATABASE [Kaizen] SET  DISABLE_BROKER 


ALTER DATABASE [Kaizen] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 


ALTER DATABASE [Kaizen] SET DATE_CORRELATION_OPTIMIZATION OFF 


ALTER DATABASE [Kaizen] SET TRUSTWORTHY OFF 


ALTER DATABASE [Kaizen] SET ALLOW_SNAPSHOT_ISOLATION OFF 


ALTER DATABASE [Kaizen] SET PARAMETERIZATION SIMPLE 


ALTER DATABASE [Kaizen] SET READ_COMMITTED_SNAPSHOT OFF 


ALTER DATABASE [Kaizen] SET HONOR_BROKER_PRIORITY OFF 


ALTER DATABASE [Kaizen] SET RECOVERY FULL 


ALTER DATABASE [Kaizen] SET  MULTI_USER 


ALTER DATABASE [Kaizen] SET PAGE_VERIFY CHECKSUM  


ALTER DATABASE [Kaizen] SET DB_CHAINING OFF 


ALTER DATABASE [Kaizen] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 


ALTER DATABASE [Kaizen] SET TARGET_RECOVERY_TIME = 0 SECONDS 


ALTER DATABASE [Kaizen] SET DELAYED_DURABILITY = DISABLED 


ALTER DATABASE [Kaizen] SET  READ_WRITE 



