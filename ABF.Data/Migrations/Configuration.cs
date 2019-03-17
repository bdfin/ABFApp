namespace ABF.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using ABFDbModels;

    internal sealed class Configuration : DbMigrationsConfiguration<ABF.Data.ABFDbModels.ABFDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ABF.Data.ABFDbModels.ABFDbContext context)
        {
            
        }
    }
}
