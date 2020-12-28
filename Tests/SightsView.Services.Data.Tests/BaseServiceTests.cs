namespace SightsView.Services.Data.Tests
{
    using System;
    using System.Reflection;

    using CloudinaryDotNet;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using SightsView.Data;
    using SightsView.Data.Common.Repositories;
    using SightsView.Data.Repositories;
    using SightsView.Services.Contracts;
    using SightsView.Services.Data.Contracts;
    using SightsView.Services.Mapping;
    using SightsView.Web.ViewModels;

    public abstract class BaseServiceTests
    {
        protected BaseServiceTests()
        {
            var services = this.SetServices();

            this.ServiceProvider = services.BuildServiceProvider();
            this.DbContext = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        }

        protected IServiceProvider ServiceProvider { get; set; }

        protected ApplicationDbContext DbContext { get; set; }

        public void Dispose()
        {
            this.DbContext.Database.EnsureDeleted();
            this.SetServices();
        }

        private ServiceCollection SetServices()
        {
            var services = new ServiceCollection();

            services.AddDbContext<ApplicationDbContext>(
                options => options.UseInMemoryDatabase(Guid.NewGuid().ToString()));

            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

            services.AddTransient<ICategoriesService, CategoriesService>();
            services.AddTransient<ICommentsService, CommentsService>();
            services.AddTransient<ICountriesService, CountriesService>();
            services.AddTransient<ICreationsService, CreationsService>();
            services.AddTransient<IDetailsService, DetailsService>();
            services.AddTransient<IEquipmentsService, EquipmentsService>();
            services.AddTransient<IFollowsService, FollowsService>();
            services.AddTransient<IMessagesService, MessagesService>();
            services.AddTransient<ITagsExtractingService, TagsExtractingService>();
            services.AddTransient<ITagsService, TagsService>();
            services.AddTransient<IPhotographersService, PhotographersService>();
            services.AddTransient<IStringHelpersService, StringHelpersService>();
            services.AddTransient<IRandomiseService, RandomiseService>();
            services.AddTransient<ILikesService, LikesService>();
            services.AddTransient<IRepliesService, RepliesService>();

            var account = new Account(
                "sightsview",
                string.Empty,
                string.Empty);
            var cloudinary = new Cloudinary(account);
            services.AddSingleton(cloudinary);

            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            return services;
        }
    }
}
