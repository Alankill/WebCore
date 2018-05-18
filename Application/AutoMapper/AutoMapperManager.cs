using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.AutoMapper
{
    public static class AutoMapperManager
    {
        public static IMapper Mapper { get; private set; }

        static AutoMapperManager()
        {
            if (Mapper == null)
            {
                CreateConfig();
            }
        }

        private static void CreateConfig()
        {
            var config = new MapperConfiguration(m =>
            {

            });

            Mapper = config.CreateMapper();
        }

    }
}
