using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations; //added for metadata and validation

namespace StoreFront.DATA.EF.StoreFront/*Metadata --- NAME MUST MATCH*/
{
    public class BeverageMetadata
    {
        //public int BeverageID { get; set; }

        [Required(ErrorMessage = "*")]
        [StringLength(50, ErrorMessage = "*Must be 50 characters or less")]
        [Display(Name = "Beverage")]
        public string BeverageName { get; set; }

        public bool Alcoholic { get; set; }

        [Range(0, double.MaxValue, ErrorMessage ="*Must be a valid number, 0 or larger")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal Price { get; set; }
    }

    [MetadataType(typeof(BeverageMetadata))]
    public partial class Beverage { }

    public class CategoryMetadata
    {
        //public int CategoryID { get; set; }

        [StringLength(50, ErrorMessage ="*Must be 50 characters or less")]
        [DisplayFormat(NullDisplayText = "---")]
        [Display(Name ="Category")]
        public string CategoryName { get; set; }

        [DisplayFormat(NullDisplayText = "---")]
        public Nullable<bool> MixTape { get; set; }
    }

    [MetadataType(typeof(CategoryMetadata))]
    public partial class Category { }

    public class EmployeeMetadata
    {
        //public int EmployeeID { get; set; }

        [Required(ErrorMessage ="*First Name is required")]
        [StringLength(50, ErrorMessage = "Must be 50 characters or less")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "*Last Name is required")]
        [StringLength(50, ErrorMessage = "Must be 50 characters or less")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [DisplayFormat(NullDisplayText = "---")]
        [Display(Name = "Direct-Report ID")]
        public Nullable<int> DirectReportID { get; set; }

        [StringLength(50, ErrorMessage = "Must be 50 characters or less")]
        [DisplayFormat(NullDisplayText ="---")]
        public string Title { get; set; }

        [StringLength(10, ErrorMessage = "Must be a valid number for hours per week")]
        [DisplayFormat(NullDisplayText ="---")]
        [Display(Name = "Hours per Week")]
        public string HoursPerWeek { get; set; }
    }

    [MetadataType(typeof(EmployeeMetadata))]
    public partial class Employee
    {
        [Display(Name = "Employee")]
        public string FullNameEmployee
        {
            get { return FirstName + " " + LastName; }
        }

    }

    public class GenreMetadata
    {
        //public int GenreID { get; set; }

        [StringLength(50, ErrorMessage="Must be 50 characters or less")]
        [DisplayFormat(NullDisplayText ="---")]
        [Display(Name ="Genre")]
        public string GenreName { get; set; }

        [Range(0, double.MaxValue, ErrorMessage="Must be a valid number, yyyy Format")]
        [DisplayFormat(NullDisplayText ="---")]
        [Display(Name ="Decade")]
        public Nullable<int> GenreDecade { get; set; }

        [StringLength(50, ErrorMessage ="Must be 50 characters or less")]
        [DisplayFormat(NullDisplayText ="---")]
        [Display(Name = "Origin Region")]
        public string OriginRegion { get; set; }

        [Required(ErrorMessage ="*Origin Country is required")]
        [StringLength(50, ErrorMessage ="Must be 50 characters or less")]
        [Display(Name = "Origin Country")]
        public string OriginCountry { get; set; }
    }

    [MetadataType(typeof(GenreMetadata))]
    public partial class Genre { }

    public class LimitedReleasesMetadata
    {
        //public int LimitID { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode =true, NullDisplayText ="---")]
        [Display(Name = "Original Release")]
        public Nullable<System.DateTime> OriginalReleaseDate { get; set; }

        [Range(0, double.MaxValue, ErrorMessage ="Please enter a valid number")]
        [DisplayFormat(NullDisplayText ="---")]
        [Display(Name = "Original Release Amount")]
        public Nullable<int> OriginalReleaseAmount { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Please enter a valid number")]
        [DisplayFormat(NullDisplayText = "---")]
        [Display(Name = "Limited Release Amount")]
        public Nullable<int> LimitedReleaseAmount { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true, NullDisplayText = "---")]
        [Display(Name = "Limited Release")]
        public Nullable<System.DateTime> LimitedReleaseDate { get; set; }
    }

    [MetadataType(typeof(LimitedReleasesMetadata))]
    public partial class LimitedReleases { }

    public class ProducerMetadata
    {
        //public int ProducerID { get; set; }

        [Required(ErrorMessage ="*First Name is required")]
        [StringLength(50, ErrorMessage= "Must be less than 50 characters")]
        [Display(Name ="First Name")]
        public string FirstName { get; set; }

        [StringLength(50, ErrorMessage = "Must be less than 50 characters")]
        [DisplayFormat(NullDisplayText ="---")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [StringLength(50, ErrorMessage = "Must be less than 50 characters")]
        [DisplayFormat(NullDisplayText = "---")]
        public string City { get; set; }

        [StringLength(2, ErrorMessage = "Please enter 2 digit state abbreviation")]
        [DisplayFormat(NullDisplayText = "---")]
        public string State { get; set; }

        [StringLength(50, ErrorMessage = "Must be less than 50 characters")]
        [DisplayFormat(NullDisplayText = "---")]
        public string Country { get; set; }
    }

    [MetadataType(typeof(ProducerMetadata))]
    public partial class Producer
    {
        [Display(Name = "Producer")]
        public string FullNameProducer
        {
            get { return FirstName + " " + LastName; }
        }
    }

    public class RecordsMetadata
    {
        //public int RecordID { get; set; }

        [Required(ErrorMessage ="*Record Name is required")]
        [StringLength(50, ErrorMessage ="Must be less than 50 characters")]
        [Display(Name ="Record")]
        public string RecordName { get; set; }

        [Required(ErrorMessage = "*Band/Musician is required")]
        [StringLength(50, ErrorMessage = "Must be less than 50 characters")]
        [Display(Name = "Band/Musician")]
        public string BandMusician { get; set; }

        [DisplayFormat(NullDisplayText ="---")]
        public Nullable<int> GenreID { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true, NullDisplayText = "---")]
        [Display(Name ="Release Date")]
        public Nullable<System.DateTime> ReleaseDate { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "*Must be a valid number, 0 or larger")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public Nullable<decimal> Price { get; set; }

        [Display(Name = "Image")]
        public string CoverImage { get; set; }

        [Required(ErrorMessage ="*Stock Status is required")]
        public int StockID { get; set; }

        [DisplayFormat(NullDisplayText = "---")]
        public Nullable<int> ProducerID { get; set; }

        [DisplayFormat(NullDisplayText = "---")]
        public Nullable<int> CategoryID { get; set; }

        [StringLength(50, ErrorMessage = "Description of Colored LP must be less than 50 characters")]
        [DisplayFormat(NullDisplayText = "---")]
        [Display(Name = "Colored LP")]
        [UIHint("MultilineText")]
        public string ColoredLP { get; set; }

        [DisplayFormat(NullDisplayText = "---")]
        public Nullable<int> LimitID { get; set; }
    }

    [MetadataType(typeof(RecordsMetadata))]
    public partial class Records { }

    public class StockStatusesMetadata
    {
        //public int StockID { get; set; }

        [Required(ErrorMessage ="*Stock Status is required")]
        [StringLength(50, ErrorMessage = "Must be less than 50 characters")]
        public string StockStatus { get; set; }
    }

    [MetadataType(typeof(StockStatusesMetadata))]
    public partial class StockStatuses { }
}
