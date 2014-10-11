namespace InverGrove.Domain.ValueTypes
{
    public static class RegularExpressions
    {
        public const string AlphaNumericRegEx = @"[^a-zA-Z0-9]";
        
        public const string ValidEmailRegEx = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/" +
            @"=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";

        /// <summary>
        /// regex look ahead defect in Internet Explorer 7
        /// rewrite the regex a work around
        /// http://blog.stevenlevithan.com/archives/regex-lookahead-bug
        /// Minimum of 8 characters, at least 1 letter, 1 number and 1 symbol is required.
        /// look ahead regex logic: ((?=.*\d)(?=.*[A-Z])(?=.*\W).{8,8})
        /// (                  # Start of group 
        ///    (?=.*\d)        #   must contain at least one digit 
        ///    (?=.*[a-zA-Z])  #   must contain at least one letter
        ///    (?=.*\W)        #   must contain at least one special symbol 
        ///       .            #   match anything with previous condition checking 
        ///         {8,}       #   length at least 8 characters, note: move to beginning to work around IE 7 defect
        ///)                   # End of group 
        /// </summary>
        public const string PasswordRegEx = @"^((?=.{8,}$)(?=.*\d)(?=.*[a-zA-Z])(?=.*\W).*)";

        public const string NonNumericRegEx = "\\D";

        public const string DashRegEx = "-";
    }
}