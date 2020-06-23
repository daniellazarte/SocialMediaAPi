using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMedia.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Infraestructure.Data.Configurations
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
       public void Configure(EntityTypeBuilder<Post> Builder)
        {
            //Aqui se configuraria si trabajas con EF. la configuracion Tabla por tabla para que no
            //se haga extenso el codigo
            throw new NotImplementedException();
        }
    }
}
