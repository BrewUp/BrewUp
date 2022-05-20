namespace BrewUpWasm.Production.Shared.Enums
{
    public sealed class ToastLevel : Enumeration
    {
        public static ToastLevel Info = new(1, "I", "Info");
        public static ToastLevel Success = new(2, "S", "Success");
        public static ToastLevel Warning = new(2, "W", "Warning");
        public static ToastLevel Error = new(2, "E", "Error");

        public static IEnumerable<ToastLevel> List() => new[] { Info, Success, Warning, Error };

        public ToastLevel(int id, string code, string name) : base(id, code, name)
        {
        }

        public static ToastLevel FromName(string name)
        {
            var toastLevel = List().SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (toastLevel == null)
                throw new Exception($"Possible values for toastLevel: {string.Join(",", List().Select(s => s.Name))}");

            return toastLevel;
        }

        public static ToastLevel FromCode(string code)
        {
            var toastLevel = List().SingleOrDefault(s => string.Equals(s.Code, code, StringComparison.CurrentCultureIgnoreCase));

            if (toastLevel == null)
                throw new Exception($"Possible values for toastLevel: {string.Join(",", List().Select(s => s.Code))}");

            return toastLevel;
        }

        public static ToastLevel From(int id)
        {
            var toastLevel = List().SingleOrDefault(s => s.Id == id);

            if (toastLevel == null)
                throw new Exception($"Possible values for toastLevel: {string.Join(",", List().Select(s => s.Name))}");

            return toastLevel;
        }
    }
}