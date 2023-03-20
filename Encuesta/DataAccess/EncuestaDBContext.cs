using Encuesta.Models;
using Microsoft.EntityFrameworkCore;

namespace Encuesta.DataAccess
{
    public class EncuestaDBContext : DbContext
    {
        public EncuestaDBContext(DbContextOptions<EncuestaDBContext> options) : base(options) { }
        public DbSet<EncuestaNpsModel>? EncuestaNps { get; set; }
    }
    
}
