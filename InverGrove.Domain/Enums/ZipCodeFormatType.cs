namespace InverGrove.Domain.Enums
{
    public enum ZipCodeFormatType
    {
        /// <summary>
        /// 55555
        /// </summary>
        UsFiveDigit = 0,

        /// <summary>
        /// 55555-5555
        /// </summary>
        UsFivePlusFourDigit,

        /// <summary>
        /// 555554444
        /// </summary>
        UsNineDigit,

        /// <summary>
        /// Any combination of above
        /// </summary>
        UsAllFormats
    }
}