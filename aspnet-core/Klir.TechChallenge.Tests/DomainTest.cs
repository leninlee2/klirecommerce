using Klir.TechChallenge.Domain.EntityModel;
using Klir.TechChallenge.Domain.Interface.Repository;
using Klir.TechChallenge.Domain.Interface.Service;
using Klir.TechChallenge.Domain.Model;
using Klir.TechChallenge.Domain.Service;
using Klir.TechChallenge.Domain.ValueObjects;
using Klir.TechChallenge.InfraStructure.ContextModel;
using Klir.TechChallenge.InfraStructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Klir.TechChallenge.Tests
{
    public class DomainTest
    {
        private IProductRepository productRepository;
        private IPromotionRepository promotionRepository;
        private IShoppingCartRepository shoppingCartRepository;
        private IClientRepository clientRepository;
        private IGroupShoopingCartRepository groupShoopingCartRepository;

        private IProductService productService;
        private IPromotionService promotionService;
        private IShoppingCartService shoppingCartService;
        private IClientService clientService;
        private IGroupShoppingCartService groupShoppingCartService;

        public Product product = new Product()
        {
            CreateDate = DateTime.Now,
            Id = Guid.NewGuid(),
            IdPromotion = new Guid(PromotionType.ByOneGetOne),
            Name = "Product B",
            Price = 20,
            Status = StatusItem.Active
        };

        public Promotion promotion = new Promotion()
        {
            CreateDate = DateTime.Now,
            Id = Guid.NewGuid(),
            Name = "Promotion - " + DateTime.Now.ToString(),
            Status = StatusItem.Active
        };

        public ShoppingCart shoppingCart = new ShoppingCart()
        {
            CreateDate = DateTime.Now,
            Id = Guid.NewGuid(),
            IdProduct = new Guid("12D4B353-CBFE-4B8D-B68B-1D9FDC9503E1"),
            IdPromotion = new Guid(PromotionType.ByOneGetOne),
            Price = 50,
            Quantity = 1,
            Status = StatusItem.Active,
            Total = 50
        };

        public Client client = new Client()
        {
            Id = Guid.NewGuid(),
            FirstName = "First " + DateTime.Now.ToString(),
            LastName = "Second " + DateTime.Now.ToString(),
            Address = "111 Address A",
            CreateDate = DateTime.Now,
            Email = "test" + new Random().Next(20, 30).ToString() + "@gmail.com",
            Password = "123",
            PhoneNumber = "3761652",
            State = "Florida",
            City = "Orlando",
            Status = StatusItem.Active,
            ZipCode = "32835"
        };

        public GroupShoopingCart groupShoopingCart = new GroupShoopingCart()
        {
            CreateDate = DateTime.Now,
            Id = Guid.NewGuid(),
            IdClient = new Guid("221B58BC-ABFD-437F-8814-546A699BE27C"),
            Status = StatusItem.Active
        };

        #region Product

        [Fact]
        public void ListProduct()
        {
            var products = productService.findAll();
            Assert.True(products.Count() >= 0);
        }

        [Fact]
        public async Task AddProductAsync()
        {
            await productService.Add(product);
            Assert.True(new Guid() != product.Id);
        }

        [Fact]
        public async Task UpdateProduct()
        {
            DateTime updateDate = DateTime.Now;
            var item = productService.findAll().First();
            item.AlterDate = updateDate;
            await productService.Update(item);
            Assert.True(updateDate == item.AlterDate);
        }

        [Fact]
        public void MockConstructorProduct()
        {
            IProductService mock = new ProductService(productRepository);
            Assert.True(mock != null);
        }

        [Fact]
        public void ListProductById()
        {
            var products = productService.findProductById(new Guid("12D4B353-CBFE-4B8D-B68B-1D9FDC9503E1"));
            Assert.True(products != null);
        }

        #endregion

        #region Promotion

        [Fact]
        public void ListPromotion()
        {
            var promotions = promotionService.findAll();
            Assert.True(promotions.Count() >= 0);
        }

        [Fact]
        public async Task AddPromotionAsync()
        {
            await promotionService.Add(promotion);
            Assert.True(new Guid() != promotion.Id);
        }


        [Fact]
        public async Task UpdatePromotionAsync()
        {
            DateTime updateTime = DateTime.Now;
            var item = getLastPromotion();
            item.AlterDate = updateTime;
            await promotionService.Update(item);
            Assert.True(new DateTime() != item.AlterDate);
        }

        private Promotion getLastPromotion()
        {
            var promotions = promotionService.findAll();
            if (promotions.Count() > 0)
                return promotions.First();
            else
                return null;
        }

        [Fact]
        public void MockPromotionConstructor()
        {
            var promotions = new PromotionService(promotionRepository);
            Assert.True(promotions != null);
        }

        #endregion

        #region Shopping

        [Fact]
        public void ListShopping()
        {
            var shoppingCarts = shoppingCartService.findAll();
            Assert.True(shoppingCarts.Count() >= 0);
        }

        [Fact]
        public async Task AddShopping()
        {
            await shoppingCartService.Add(shoppingCart);
            Assert.True(new Guid() != shoppingCart.Id);
        }

        [Fact]
        public async Task UpdateShopping()
        {
            var item = getLastShopping();
            item.AlterDate = DateTime.Now;
            await shoppingCartService.Update(item);
            Assert.True(new DateTime() != item.AlterDate);
        }

        private ShoppingCart getLastShopping()
        {
            var result = shoppingCartService.findAll();
            if (result.Count() > 0)
                return result.First();
            else
                return null;
        }

        [Fact]
        public void MockConstructorShopping()
        {
            var shoppingCarts = new ShoppingCartService(shoppingCartRepository, productRepository, promotionRepository);
            Assert.True(shoppingCarts != null);
        }

        [Fact]
        public void ListShoppingComplete()
        {
            var shoppingCarts = shoppingCartService.findAllComplete();
            Assert.True(shoppingCarts.Count() >= 0);
        }

        [Fact]
        public void UpdateValueShopping()
        {
            ShoppingCartRequest req = new ShoppingCartRequest() { Id = "C258A65B-0964-4EEE-A133-0A9F77DA2B22", Quantity=15 };
            var id = shoppingCartService.UpdateValue(req).Result;
            Assert.True(new Guid() != id);
        }

        [Fact]
        public void InactiveShopping()
        {
            ShoppingCartRequest req = new ShoppingCartRequest() { Id = "C258A65B-0964-4EEE-A133-0A9F77DA2B22" };
            var id = shoppingCartService.Inactive(req).Result;
            Assert.True(new Guid() != id);
        }


        #endregion

        #region Client

        [Fact]
        public void ListClient()
        {
            var clients = clientService.findAll();
            Assert.True(clients.Count() >= 0);
        }

        [Fact]
        public async Task AddClient()
        {
            await clientRepository.AddClient(client);
            Assert.True(new Guid() != shoppingCart.Id);
        }


        [Fact]
        public void AuthenticateClient()
        {
            var clients = clientService.findByLoginAndPassword("test17@gmail.com", "123");
            Assert.True(clients != null);
        }

        [Fact]
        public async Task UpdateClientAsync()
        {
            var clientLocal = clientService.findByLoginAndPassword("test17@gmail.com", "123");
            clientLocal.AlterDate = DateTime.Now;
            await clientService.Update(clientLocal);
            Assert.True(new Guid() != clientLocal.Id);
        }

        [Fact]
        public void MockClientConstructor()
        {
            IClientService clientService = new ClientService(clientRepository);
            Assert.True(clientService != null);
        }

        [Fact]
        public void ListClientById()
        {
            var client = clientService.findAllById(new Guid("1F2238DF-DA6B-446F-9DBB-E96EDEA2F6D1"));
            Assert.True(client != null);
        }

        [Fact]
        public void InactiveClient()
        {
            var id = clientService.Inactive(new Guid("221B58BC-ABFD-437F-8814-546A699BE27C"));
            Assert.True(id != null);
        }

        [Fact]
        public void UpdateClientDetail()
        {
            ClientRequest clientRequest = new ClientRequest() { Id = "221B58BC-ABFD-437F-8814-546A699BE27C", FirstName="Test" };
            var id = clientService.Update(clientRequest);
            Assert.True(id != null);
        }

        #endregion

        #region GroupShoppingCart

        [Fact]
        public async Task AddGroupShoppingCart()
        {
            await groupShoppingCartService.Add(groupShoopingCart);
            Assert.True(new Guid() != groupShoopingCart.Id);
        }

        [Fact]
        public void ListGroupShoppingCart()
        {
            var groups = groupShoppingCartService.findAll();
            Assert.True(groups.Count() >= 0);
        }

        [Fact]
        public async Task UpdateGroup()
        {
            var item = groupShoppingCartService.findAll().FirstOrDefault();
            item.AlterDate = DateTime.Now;
            await groupShoppingCartService.Update(item);
            Assert.True(new Guid() != item.Id);
        }

        [Fact]
        public void MockGroupShoppingCartConstructor()
        {
            IGroupShoppingCartService mock = new GroupShoppingCartService(groupShoopingCartRepository);
            Assert.True(mock != null);
        }

        [Fact]
        public async Task AddGroupShoppingCartOnlyClient()
        {
            GroupShoopingCartRequest groupShoopingCartRequest = new GroupShoopingCartRequest() { IdClient = "221B58BC-ABFD-437F-8814-546A699BE27C" };
            var id = await groupShoppingCartService.Add(groupShoopingCartRequest);
            Assert.True(new Guid() != id);
        }

        [Fact]
        public async Task CloseGroup()
        {
            GroupShoopingCartRequest groupShoopingCartRequest = new GroupShoopingCartRequest() { Id = "A7BF3B1D-DE6C-4813-BDBC-43FCDB47154B" };
            var id = await groupShoppingCartService.Close(groupShoopingCartRequest);
            Assert.True(new Guid() != id);
        }

        [Fact]
        public async Task InactiveGroup()
        {
            GroupShoopingCartRequest groupShoopingCartRequest = new GroupShoopingCartRequest() { Id = "A7BF3B1D-DE6C-4813-BDBC-43FCDB47154B" };
            var id = await groupShoppingCartService.Inactive(groupShoopingCartRequest);
            Assert.True(new Guid() != id);
        }

        #endregion

        public DomainTest()
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


        }

    }

}
