using System.Security.Cryptography;
using System.Text;

namespace BrewUpWasm.Production.Shared.Concretes
{
    public class CommonServices
    {
        private static readonly Random Random = new ();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!._";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[Random.Next(s.Length)]).ToArray());
        }

        public static string GetErrorMessage(Exception ex)
        {
            return GetMessageFromException(ex);
        }

        public static string GetDefaultErrorTrace(Exception ex)
        {
            return $"Source: {ex.Source} StackTrace: {ex.StackTrace} Message: {GetMessageFromException(ex)}";
        }

        private static string GetMessageFromException(Exception ex)
        {
            while (true)
            {
                if (ex.InnerException == null)
                    return ex.Message;

                ex = ex.InnerException;
            }
        }

        public static string GetEventDate()
        {
            return $"{DateTime.UtcNow}";
        }

        public static string ChkStringNull(string stringToChk, string stringToReplace = "")
        {
            return !string.IsNullOrEmpty(stringToChk) ? stringToChk :
                !string.IsNullOrEmpty(stringToReplace) ? stringToReplace : string.Empty;
        }

        public static string GetHashingPassword(string password)
        {
            using (var algorithm = SHA256.Create())
            {
                // Create the at_hash using the access token returned by CreateAccessTokenAsync.
                //var hash = algorithm.ComputeHash(Encoding.ASCII.GetBytes(response.AccessToken));
                var hash = algorithm.ComputeHash(Encoding.ASCII.GetBytes(password));

                return Encoding.Unicode.GetString(hash, 0, hash.Length);
                // Note: only the left-most half of the hash of the octets is used.
                // See http://openid.net/specs/openid-connect-core-1_0.html#CodeIDToken
            }
        }

        public static Guid GetNewGuid()
        {
            var destinationArray = Guid.NewGuid().ToByteArray();
            var time = new DateTime(0x76c, 1, 1);
            var now = DateTime.Now;
            var span = new TimeSpan(now.Ticks - time.Ticks);
            var timeOfDay = now.TimeOfDay;
            var bytes = BitConverter.GetBytes(span.Days);
            var array = BitConverter.GetBytes((long)(timeOfDay.TotalMilliseconds / 3.333333));
            Array.Reverse(bytes);
            Array.Reverse(array);
            Array.Copy(bytes, bytes.Length - 2, destinationArray, destinationArray.Length - 6, 2);
            Array.Copy(array, array.Length - 4, destinationArray, destinationArray.Length - 4, 4);
            return new Guid(destinationArray);
        }

        public static Guid GetGuidFromString(string stringToConvert)
        {
            Guid.TryParse(stringToConvert, out var stringConverted);

            return stringConverted;
        }
    }
}