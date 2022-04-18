using Klir.TechChallenge.Domain.EntityModel;
using Klir.TechChallenge.Domain.Interface.Repository;
using Klir.TechChallenge.Domain.ValueObjects;
using Klir.TechChallenge.InfraStructure.ContextModel;
using Klir.TechChallenge.InfraStructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;
using System.Linq;
using Klir.TechChallenge.Domain.Interface.Service;
using Klir.TechChallenge.Domain.Service;
using Klir.TechChallenge.Domain.Interface.Controller;
using Klir.TechChallenge.Web.Api.Controllers;
using Klir.TechChallenge.Domain.Model;

namespace Klir.TechChallenge.Tests
{
    public class WebAPITest
    {
        //repositories
        private IProductRepository productRepository;
        private IPromotionRepository promotionRepository;
        private IShoppingCartRepository shoppingCartRepository;
        private IClientRepository clientRepository;
        private IGroupShoopingCartRepository groupShoopingCartRepository;

        //services
        private IProductService productService;
        private IPromotionService promotionService;
        private IShoppingCartService shoppingCartService;
        private IClientService clientService;
        private IGroupShoppingCartService groupShoppingCartService;

        //controllers:
        private IClientController clientController;
        private IProductController productController;
        private IPromotionController promotionController;
        private IShoppingCartController shoppingCartController;

        //public static List<Promotion> Promotions = new List<Promotion> {
        //      new Promotion() { CreateDate=DateTime.Now,Id= new Guid(PromotionType.ByOneGetOne),Name= "Buy 1 Get 1 Free",Status=true },
        //      new Promotion() { CreateDate=DateTime.Now,Id= new Guid(PromotionType.ByThreeForTen),Name= "3 for 10 Euro" ,Status=true }
        //};

        //public static List<Promotion> PromotionsSecond = new List<Promotion> {
        //      new Promotion() { CreateDate=DateTime.Now,Id= Guid.NewGuid(),Name= "Example -" + DateTime.Now.ToString(),Status=true }
        //};

        //public Product product = new Product()
        //{
        //    CreateDate = DateTime.Now,
        //    Id = Guid.NewGuid()
        //    ,
        //    IdPromotion = new Guid(PromotionType.ByOneGetOne)
        //    ,
        //    Name = "Product A",
        //    Price = 10,
        //    Status = true
        //};

        //public Client client = new Client()
        //{
        //    Id = Guid.NewGuid(),
        //    FirstName = "First " + DateTime.Now.ToString(),
        //    LastName = "Second " + DateTime.Now.ToString(),
        //    Address = "111 Address A",
        //    CreateDate = DateTime.Now,
        //    Email = "test" + new Random().Next(1, 20).ToString() + "@gmail.com",
        //    Password = "123",
        //    PhoneNumber = "3761652",
        //    State = "Florida",
        //    City = "Orlando",
        //    Status = StatusItem.Active,
        //    ZipCode = "32835"
        //};

        //public Product productC = new Product()
        //{
        //    CreateDate = DateTime.Now,
        //    Id = Guid.NewGuid(),
        //    IdPromotion = new Guid(PromotionType.ByOneGetOne),
        //    Name = "Product C",
        //    Price = 25,
        //    Status = true
        //};

        public ShoppingCartRequest shoppingCart = new ShoppingCartRequest()
        {
           IdClient= "221B58BC-ABFD-437F-8814-546A699BE27C",
           IdProduct= "12D4B353-CBFE-4B8D-B68B-1D9FDC9503E1",
           Quantity=5,
           Id = "C258A65B-0964-4EEE-A133-0A9F77DA2B22"
        };

        //public GroupShoopingCart groupShoopingCart = new GroupShoopingCart()
        //{
        //    CreateDate = DateTime.Now,
        //    Id = Guid.NewGuid(),
        //    IdClient = new Guid("1F2238DF-DA6B-446F-9DBB-E96EDEA2F6D1"),
        //    Status = StatusItem.Active
        //};

        #region Client

        [Fact]
        public void ClientGet()
        {
            var client = clientController.Get("221B58BC-ABFD-437F-8814-546A699BE27C");
            Assert.True(client != null);
        }

        [Fact]
        public void AuthenticateClient()
        {
            var client = clientController.Authenticate("leninlee@hotmail.com", "1234");
            Assert.True(client != null);
        }

        [Fact]
        public void PostClient()
        {
            ClientRequest entry = new ClientRequest() { Email="jordan" + new Random().Next(100, 200).ToString() + "@gmail.com",Password="1234" };
            var client = clientController.Post(entry);
            Assert.True(client != null);
        }

        [Fact]
        public void InactiveClient()
        {
            ClientRequest entry = new ClientRequest() { Id= "221B58BC-ABFD-437F-8814-546A699BE27C" };
            var client = clientController.Inactive(entry);
            Assert.True(client != null);
        }

        [Fact]
        public void UpdateClient()
        {
            ClientRequest entry = new ClientRequest() { Id = "221B58BC-ABFD-437F-8814-546A699BE27C", FirstName="Test" };
            var client = clientController.Update(entry);
            Assert.True(client != null);
        }

        [Fact]
        public void MockContructorClient()
        {
            IClientController mock = new ClientController(clientService);
            Assert.True(mock != null);
        }

        #endregion

        #region Product

        [Fact]
        public void MockContructorProduct()
        {
            IProductController mock = new ProductController(productService);
            Assert.True(mock != null);
        }

        [Fact]
        public void GetProduct()
        {
            var products = productController.Get();
            Assert.True(products.Count() >= 0);
        }


        [Fact]
        public void GetProductById()
        {
            var products = productController.GetById(new Guid("12D4B353-CBFE-4B8D-B68B-1D9FDC9503E1"));
            Assert.True(products != null);
        }


        #endregion

        #region Promotion

        [Fact]
        public void MockContructorPromotion()
        {
            IPromotionController mock = new PromotionController(promotionService);
            Assert.True(mock != null);
        }

        [Fact]
        public void GetPromotion()
        {
            var result = promotionController.Get();
            Assert.True(result.Count() >= 0);
        }

        #endregion

        #region ShoppingCartController

        [Fact]
        public void MockContructorShopping()
        {
            IShoppingCartController mock = new ShoppingCartController(shoppingCartService,groupShoopingCartRepository,groupShoppingCartService);
            Assert.True(mock != null);
        }

        [Fact]
        public void GetShopping()
        {
            var result = shoppingCartController.Get("C258A65B-0964-4EEE-A133-0A9F77DA2B22");
            Assert.True(result.Count() >= 0);
        }


        [Fact]
        public void PostShopping()
        {
            var result = shoppingCartController.Post(shoppingCart);
            Assert.True(result != null);
        }

        [Fact]
        public void InactiveShopping()
        {
            var result = shoppingCartController.Inactive(shoppingCart);
            Assert.True(result != null);
        }

        [Fact]
        public void UpdateShopping()
        {
            var result = shoppingCartController.Update(shoppingCart);
            Assert.True(result != null);
        }

        [Fact]
        public void CloseShopping()
        {
            var result = shoppingCartController.CloseCart(shoppingCart);
            Assert.True(result != null);
        }

        #endregion

        public WebAPITest()
        {


            StreamReader r = new StreamReader("appsettings.json");
            string jsonString = r.ReadToEnd();
            AppSettings appSettings = JsonConvert.DeserializeObject<AppSettings>(jsonString);

            var services = new ServiceCollection();
            services.AddDbContext<KlirContext>(
                        options => options.UseSqlServer(appSettings.ConnectionStrings.Main));

            //repository
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IPromotionRepository, PromotionRepository>();
            services.AddTransient<IShoppingCartRepository, ShoppingCartRepository>();
            services.AddTransient<IClientRepository, ClientRepository>();
            services.AddTransient<IGroupShoopingCartRepository, GroupShoopingCartRepository>();

            //services
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IPromotionService, PromotionService>();
            services.AddTransient<IShoppingCartService, ShoppingCartService>();
            services.AddTransient<IClientService, ClientService>();
            services.AddTransient<IGroupShoppingCartService, GroupShoppingCartService>();

            //controllers
            services.AddTransient<IClientController, ClientController>();
            services.AddTransient<IProductController, ProductController>();
            services.AddTransient<IPromotionController, PromotionController>();
            services.AddTransient<IShoppingCartController, ShoppingCartController>();

            var serviceProvider = services.BuildServiceProvider();

            productRepository = serviceProvider.GetService<IProductRepository>();
            promotionRepository = serviceProvider.GetService<IPromotionRepository>();
            shoppingCartRepository = serviceProvider.GetService<IShoppingCartRepository>();
            clientRepository = serviceProvider.GetService<IClientRepository>();
            groupShoopingCartRepository = serviceProvider.GetService<IGroupShoopingCartRepository>();

            productService = serviceProvider.GetService<IProductService>();
            promotionService = serviceProvider.GetService<IPromotionService>();
            shoppingCartService = serviceProvider.GetService<IShoppingCartService>();
            clientService = serviceProvider.GetService<IClientService>();
            groupShoppingCartService = serviceProvider.GetService<IGroupShoppingCartService>();

            clientController = serviceProvider.GetService<IClientController>();
            productController = serviceProvider.GetService<IProductController>();
            promotionController = serviceProvider.GetService<IPromotionController>();
            shoppingCartController = serviceProvider.GetService<IShoppingCartController>();

        }
    }
}
