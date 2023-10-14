using AutoMapper;
using System.Reflection;

namespace ServerArchitecture.Common
{
    public class DtoConverter
    {
        private static MapperConfiguration AutoMapperConfiguration { get; set; }

        public static void Init()
        {
            var ass = Assembly.GetExecutingAssembly();
            AutoMapperConfiguration = new MapperConfiguration(c =>
                c.AddProfiles((IEnumerable<Profile>)ass));
        }

        public TOutput ConvertTo<TOutput>(object input) where TOutput : class
        {
            if (input == null) return null;

            var autoMapper = AutoMapperConfiguration.CreateMapper();

            try
            {
                return autoMapper.Map<TOutput>(input);
            }
            catch (AutoMapperMappingException ex)
            {
                throw new DTOConversionException(ex.Message, ex);
            }
        }

        public ICollection<TOutput> ConvertTo<TOutput>(IEnumerable<object> input) where TOutput : class
        {
            return input.Select(i => ConvertTo<TOutput>(i)).ToList();
        }


        public TOutput Convert<TOutput, TInput>(
            Func<TInput, TOutput> converter, TInput input) where TOutput : class
        {
            if (input == null) return null;

            return converter(input);
        }

        public ICollection<TOutput> Convert<TOutput, TInput>(
            Func<TInput, TOutput> converter, IEnumerable<TInput> input) where TOutput : class
        {
            return input.Select(i => Convert(converter, i)).ToList();
        }
    }

    [Serializable]
    public class DTOConversionException : Exception
    {
        public DTOConversionException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
