using System.ComponentModel.DataAnnotations;

namespace Bookly.Models;




public class Book
{
    public int Id { set; get; }
    
    [Required]
    public string title { set; get; }
    
    [MaxLength(100)]
    public string desc { set; get; }
    
    public string lang { set; get; }

    [Required,MinLength(13)]
    public string  isbn { set; get; }
    
    [Required,DataType(DataType.Date),Display(Name = "Date published")]
    public string datePub { set; get; }
    
    [Required,DataType(DataType.Currency)]
    public int price { set; get; }
    
    [Required]
    public string  author { set; get; }
    
    [Display(Name ="Image url")]
    public string urlImg { set; get; }
    

    
    
    
}