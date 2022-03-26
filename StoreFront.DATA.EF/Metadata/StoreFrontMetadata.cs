using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreFront.DATA.EF/*.Metadata*/
{

    public class BeverageMetadata
    {      
        //public int BeverageID { get; set; }

        [Required(ErrorMessage ="Beverage Name is required")]
        [StringLength(50, ErrorMessage ="Must be less than 50 characters")]
        [Display(Name = "Beverage")]
        public string BeverageName { get; set; }

        [Required(ErrorMessage = "Acoholic is required")]
        public bool Alcoholic { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [DisplayFormat(DataFormatString ="{0:c}")]
        public decimal Price { get; set; }
    }
    [MetadataType(typeof(BeverageMetadata))]
    public partial class Beverage { }


    public class CategoryMetadata
    {
        //public int CategoryID { get; set; }

        [Display(Name = "Category")]
        [DisplayFormat(NullDisplayText = "---")]
        public string CategoryName { get; set; }

        [DisplayFormat(NullDisplayText = "---")]
        public Nullable<bool> MixTape { get; set; }
    }
    [MetadataType(typeof(CategoryMetadata))]
    public partial class Category { }


    public class EmployeeMetadata
    {
        //public int EmployeeID { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [Display(Name = "First Name")]
        [StringLength(50, ErrorMessage ="Must be 50 characters or less")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [Display(Name = "Last Name")]
        [StringLength(50, ErrorMessage ="Must be 50 characters or less")]
        public string LastName { get; set; }

        [DisplayFormat(NullDisplayText = "---")]
        public Nullable<int> DirectReportID { get; set; }

        [DisplayFormat(NullDisplayText ="---")]
        [StringLength(50, ErrorMessage ="Must be 50 characters or less")]
        public string Title { get; set; }

        [DisplayFormat(NullDisplayText = "---")]
        [StringLength(10, ErrorMessage ="Enter as numeral")]
        public string HoursPerWeek { get; set; }
    }
    [MetadataType(typeof(EmployeeMetadata))]
    public partial class Employee { }


    public class GenreMetadata
    {
        //public int GenreID { get; set; }

        [DisplayFormat(NullDisplayText = "---")]
        [StringLength(50, ErrorMessage ="Must be 50 characters or less")]
        public string GenreName { get; set; }

        [DisplayFormat(NullDisplayText = "---")]
        public Nullable<int> GenreDecade { get; set; }

        [DisplayFormat(NullDisplayText = "---")]
        [StringLength(50, ErrorMessage ="Must be 50 characters or less")]
        public string OriginRegion { get; set; }

        [Required(ErrorMessage = "Origin Country is required")]
        [StringLength(50, ErrorMessage = "Must be 50 characters or less")]
        public string OriginCountry { get; set; }
    }
    [MetadataType(typeof(GenreMetadata))]
    public partial class Genre { }


    public class ProducerMetadata
    {
        //public int ProducerID { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [StringLength(50, ErrorMessage ="Must be 50 characters or less")]
        public string FirstName { get; set; }

        [DisplayFormat(NullDisplayText = "---")]
        [StringLength(50, ErrorMessage = "Must be 50 characters or less")]
        public string LastName { get; set; }

        [DisplayFormat(NullDisplayText = "---")]
        [StringLength(50, ErrorMessage = "Must be 50 characters or less")]
        public string City { get; set; }

        [DisplayFormat(NullDisplayText = "---")]
        [StringLength(2, ErrorMessage = "Please enter 2 digit state abbreviation")]
        public string State { get; set; }

        [DisplayFormat(NullDisplayText = "---")]
        [StringLength(50, ErrorMessage = "Must be 50 characters or less")]
        public string Country { get; set; }
    }
    [MetadataType(typeof(ProducerMetadata))]
    public partial class Producer { }


    public class RecordMetadata
    {
        //public int RecordID { get; set; }

        [Required(ErrorMessage = "Record Name is required")]
        [StringLength(50, ErrorMessage = "Must be 50 characters or less")]
        public string RecordName { get; set; }

        [Required(ErrorMessage = "Beverage Name is required")]
        [StringLength(50, ErrorMessage = "Must be 50 characters or less")]
        public string BandMusician { get; set; }

        [DisplayFormat(NullDisplayText = "---")]
        public Nullable<int> GenreID { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}", NullDisplayText = "---")]
        public Nullable<System.DateTime> ReleaseDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}", NullDisplayText = "---")]
        public Nullable<decimal> Price { get; set; }

        [DisplayFormat(NullDisplayText = "---")]
        public string CoverImage { get; set; }

        [Required(ErrorMessage = "Stock Status is required")]
        public int StockID { get; set; }

        [DisplayFormat(NullDisplayText = "---")]
        public Nullable<int> ProducerID { get; set; }

        [DisplayFormat(NullDisplayText = "---")]
        public Nullable<int> CategoryID { get; set; }

        [DisplayFormat(NullDisplayText = "---")]
        [StringLength(100, ErrorMessage ="Must be 100 characters or less")]
        public string ColoredLP { get; set; }
    }
    [MetadataType(typeof(RecordMetadata))]
    public partial class Record { }


    public class StockStatusMetadata
    {
        //public int StockID { get; set; }

        [Required(ErrorMessage = "Stock Status is required")]
        [StringLength(50, ErrorMessage ="Must be 50 characters or less")]
        public string Status { get; set; }
    }
    [MetadataType(typeof(StockStatusMetadata))]
    public partial class StockStatus { }
    


}

