// ReSharper disable InconsistentNaming
namespace ClassLibrary2
{
  
        public class ErrorClass
        {
            public string Description { get; set; }
            public ErrorList ErrorCode { get; set; }
        }
        public enum ErrorList
        {
            OK = 0,
            PRIVACY_ERROR = 1,
            ERROR_DUPLICATION = 2,
        }
}