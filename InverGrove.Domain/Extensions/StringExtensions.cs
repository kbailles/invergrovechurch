using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using InverGrove.Domain.Enums;
using InverGrove.Domain.Exceptions;
using InverGrove.Domain.ValueTypes;

namespace InverGrove.Domain.Extensions
{
    public static class StringExtensions
    {
        private static readonly Regex alphaNumeric = new Regex(RegularExpressions.AlphaNumericRegEx, 
            RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);

        private static readonly Regex validEmail = new Regex(RegularExpressions.ValidEmailRegEx,
           RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);

        private static readonly Regex nonNumerics = new Regex(RegularExpressions.NonNumericRegEx, RegexOptions.Compiled);
        private const int EmailAddressMaximumLength = 254;

        /// <summary>
        /// Formats the string as a US phone number.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        public static string FormatUsPhoneNumber(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return s;
            }

            s = RemoveNonNumericCharacters(s);
            if (s.Length > 0)
            {
                if (s.Length == 10)
                {
                    return "(" + s.Substring(0, 3) + ") " + s.Substring(3, 3) + "-" + s.Substring(6, 4);
                }

                return s.Substring(0, 3) + "-" + s.Substring(3, 4);
            }

            return "";
        }

        /// <summary>
        /// Formats the string as a US phone number.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="extensionLength">Length of the extension.</param>
        /// <param name="includeExtension">if set to <c>true</c> [include extension].</param>
        /// <returns></returns>
        public static string FormatFullPhoneNumber(this string s, int extensionLength = 0, bool includeExtension = false)
        {
            if (string.IsNullOrEmpty(s))
            {
                return s;
            }

            string extension = null;

            //Number:   1 8 0 0 5 5 5 1 2  1  2 |  x  1  2  3  4
            //---------------------------------------------------
            //Length:   1 2 3 4 5 6 7 8 9 10 11 | 12 13 14 15 16
            //Index:    0 1 2 3 4 5 6 7 8  9 10 | 11 12 13 14 15
            if (extensionLength > 0)
            {
                int split = s.Length - extensionLength; //ex: 16 - 5 == 11
                extension = s.Substring(split);
                s = s.Substring(0, split).RemoveNonNumericCharacters();
            }
            else
            {
                s = s.RemoveNonNumericCharacters();
            }

            StringBuilder sb = new StringBuilder();
            int range = s.Length;
            while (range > 0)
            {
                if (range >= 11)
                {
                    int length = (range + 1) - 11;
                    sb.AppendFormat("{0} ", s.Substring(0, length));
                    range -= length;
                }
                else if (range == 10)
                {
                    sb.AppendFormat("({0}) ", s.Substring(s.Length - 10, 3));
                    range -= 3;
                }
                else if (range == 7)
                {
                    sb.AppendFormat("{0}-{1}", s.Substring(s.Length - 7, 3), s.Substring(s.Length - 4, 4));
                    range -= 7;
                }
                else
                {
                    sb.Append(s);
                    range = 0;
                }
            }

            if (includeExtension && extension != null)
            {
                sb.AppendFormat(" {0}", extension);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Determines whether email address entered is valid.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns>
        /// 	<c>true</c> if [is valid email] otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValidEmail(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return false;
            }

            if (s.Length > EmailAddressMaximumLength)
            {
                return false;
            }

            return validEmail.IsMatch(s);
        }

        /// <summary>
        /// Determines whether [is valid phone number] [the specified s].
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="formatType">The format.</param>
        /// <returns>
        /// 	<c>true</c> if [is valid phone number] [the specified s]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValidPhoneNumber(this string s, PhoneNumberFormatType formatType)
        {
            string pattern = "";

            if (string.IsNullOrEmpty(s))
            {
                return false;
            }

            switch (formatType)
            {
                case PhoneNumberFormatType.UsAllFormats:
                    pattern = @"^(1\s*[-\/\.]?)?(\((\d{3})\)|(\d{3}))\s*[-\/\.]?\s*(\d{3})\s*[-\/\.]?\s*(\d{4})\s*(([xX]|[eE][xX][tT])\.?\s*(\d+))*$";
                    break;
            }

            Regex match = new Regex(pattern);

            return match.IsMatch(s);
        }

        /// <summary>
        /// Determines whether [is valid zip code] [the specified s].
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="formatType">Type of the format.</param>
        /// <returns>
        ///   <c>true</c> if [is valid zip code] [the specified s]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValidZipCode(this string s, ZipCodeFormatType formatType)
        {
            string pattern = "";
            switch (formatType)
            {
                case ZipCodeFormatType.UsFiveDigit:
                    pattern = @"^(\d{5})$";
                    break;
                case ZipCodeFormatType.UsFivePlusFourDigit:
                    pattern = @"^(\d{5}-\d{4})$";
                    break;
                case ZipCodeFormatType.UsNineDigit:
                    pattern = @"^(\d{9})$";
                    break;
                case ZipCodeFormatType.UsAllFormats:
                    pattern = @"^(\d{5}-\d{4}|\d{5}|\d{9})$";
                    break;
            }

            Regex match = new Regex(pattern);

            return match.IsMatch(s);
        }

        /// <summary>
        /// Strips the phone string.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        public static string StripPhoneString(this string s)
        {
            string newValue = string.Empty;

            if (!string.IsNullOrEmpty(s))
            {
                foreach (char ch in s)
                {
                    if (ch != ' ' && ch != '(' && ch != ')' && ch != '-' && ch != '.')
                    {
                        if (char.IsNumber(ch))
                        {
                            newValue += ch;
                        }
                    }

                }
            }
            return newValue;
        }

        /// <summary>
        /// Creates a one way hash for string passed using the Sha256 hashing algorithm.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        public static string Sha256Hash(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return s;
            }

            SHA256 sha256 = SHA256.Create();
            byte[] dataSha256 = sha256.ComputeHash(Encoding.Default.GetBytes(s));
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < dataSha256.Length; i++)
            {
                sb.AppendFormat("{0:x2}", dataSha256[i]);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Formats the passcode.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="format">The format.</param>
        /// <param name="salt">The salt.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public static string FormatPasscode(this string value, InverGrovePasswordFormat format, string salt = "")
        {
            switch (format)
            {
                case InverGrovePasswordFormat.Clear:
                    return value;
                case InverGrovePasswordFormat.Hashed:
                    return value.HashPasscode(salt);
                case InverGrovePasswordFormat.Encrypted:
                    throw new NotImplementedException();
                default:
                    return value;
            }
        }

        /// <summary>
        ///   Hashes the passcode.
        /// </summary>
        /// <param name = "s">The s.</param>
        /// <param name = "salt">The salt.</param>
        /// <returns></returns>
        public static string HashPasscode(this string s, string salt)
        {
            if (s == null)
            {
                throw new ParameterNullException("s");
            }

            if (salt == null)
            {
                throw new ParameterNullException("salt");
            }

            string passcode = s;
            return Sha256Hex(salt + passcode);
        }

        /// <summary>
        /// Allow only alpha numeric characters
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        public static string ToAlphaNumeric(this string s)
        {
            return string.IsNullOrEmpty(s) ? s : alphaNumeric.Replace(s, "");
        }

        /// <summary>
        /// Toes the security answer.
        /// Answer will be stripped off of all punctuations, special characters and white spaces, converted to lowercase
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        public static string ToSecurityAnswer(this string s)
        {
            return string.IsNullOrEmpty(s) ? s : alphaNumeric.Replace(s, "").ToLowerInvariant();
        }

        /// <summary>
        /// Determines whether [is valid hash value] [the specified s].
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        public static bool IsValidHashValue(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return false;
            }

            return s.Length == 128;
        }

        /// <summary>
        ///   Returns a random 64 character hex string (256 bits)
        /// </summary>
        /// <param name = "s">The s.</param>
        /// <returns></returns>
        public static string GetRandomSalt(this string s)
        {
            //256 bits
            byte[] salt = new byte[32];

            RNGCryptoServiceProvider random = new RNGCryptoServiceProvider();
            random.GetBytes(salt);
            return BytesToHex(salt);
        }

        /// <summary>
        ///   Bytes to hex.
        /// </summary>
        /// <param name = "toConvert">To convert.</param>
        /// <returns></returns>
        private static string BytesToHex(byte[] toConvert)
        {
            StringBuilder s = new StringBuilder(toConvert.Length * 2);
            foreach (byte b in toConvert)
            {
                s.Append(b.ToString("x2"));
            }
            return s.ToString();
        }

        /// <summary>
        /// Returns an item number with no characters, only numbers
        /// </summary>
        /// <param name="number">The number.</param>
        /// <returns></returns>
        public static string RemoveNonNumericCharacters(this string number)
        {
            return nonNumerics.Replace(number, "");
        }

        /// <summary>
        ///   Sha256s the hex.
        /// </summary>
        /// <param name = "toHash">To hash.</param>
        /// <returns></returns>
        private static string Sha256Hex(string toHash)
        {
            SHA256Managed hash = new SHA256Managed();
            byte[] utf8 = Encoding.UTF8.GetBytes(toHash);
            return BytesToHex(hash.ComputeHash(utf8));
        }
    }
}