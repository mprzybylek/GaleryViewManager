using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GalleryViewManager.DataAccess
{
    public class ImageContext: DbContext
    {
        public virtual DbSet<ImageEntity> Images { get; set; }

        protected override void OnConfiguring(
           DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(
                "Data Source=Galery.sqlite");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
