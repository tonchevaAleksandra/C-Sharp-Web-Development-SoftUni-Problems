using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AspNetAppForTestingRazor.ViewModels.Home
{
    public class IndexViewModel
    {
        [Range(2000,2050)]
        public int Year { get; set; }
        public int UsersCount { get; set; }
        public int ProcessorsCount { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }
}
