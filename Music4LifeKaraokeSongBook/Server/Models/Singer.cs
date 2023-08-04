using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

public class Singer
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public ICollection<Song> Songs { get; set; } = new List<Song>();
}