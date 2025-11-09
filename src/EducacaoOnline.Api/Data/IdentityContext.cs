using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EducacaoOnline.Api.Data
{
    public class IdentityContext(DbContextOptions<IdentityContext> options) : IdentityDbContext(options) { }
}