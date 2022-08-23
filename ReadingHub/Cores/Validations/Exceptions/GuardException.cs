using ReadingHub.Cores.Validations.Attributes;

namespace ReadingHub.Cores.Validations.Exceptions
{
    public static class GuardException
    {
        public static void NotNull<T>([ValidatedNotNull] this T value, string name) where T : class
        {
            if (value == null)
                throw new ArgumentNullException(name);
        }

        public static void NotFound<T>([ValidatedNotNull] this T value, string name) where T : class
        {
            if (value == null)
                throw new NotFoundException(name);


        }

        public static void CanNotCreate(bool isNull , string name){ 
        if(isNull)
                throw new CanNotCreateException(name);
        }
    }
}
