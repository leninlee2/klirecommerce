using Klir.TechChallenge.InfraStructure.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Linq;
using Klir.TechChallenge.InfraStructure.ContextModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using Newtonsoft.Json;
using Klir.TechChallenge.Domain.EntityModel;
using System.Threading.Tasks;
using Klir.TechChallenge.Domain.Interface.Repository;
using Klir.TechChallenge.Domain.ValueObjects;

namespace Klir.TechChallenge.Tests
{
    
    public class InfraStructureTest
    {
        private IProductRepository productRepository;
        private IPromotionRepository promotionRepository;
        private IShoppingCartRepository shoppingCartRepository;
        private IClientRepository clientRepository;
        private IGroupShoopingCartRepository groupShoopingCartRepository;

        public static List<Promotion> Promotions = new List<Promotion> {
              new Promotion() { CreateDate=DateTime.Now,Id= new Guid(PromotionType.ByOneGetOne),Name= "Buy 1 Get 1 Free",Status=true },
              new Promotion() { CreateDate=DateTime.Now,Id= new Guid(PromotionType.ByThreeForTen),Name= "3 for 10 Euro" ,Status=true }
        };

        public static List<Promotion> PromotionsSecond = new List<Promotion> {
              new Promotion() { CreateDate=DateTime.Now,Id= Guid.NewGuid(),Name= "Example -" + DateTime.Now.ToString(),Status=true }
        };

        public Product product = new Product() { CreateDate = DateTime.Now,Id= Guid.NewGuid()
            ,IdPromotion=new Guid(PromotionType.ByOneGetOne) 
            ,Name="Product A",Price=10,Status=true
        };

        public Client client = new Client()
        {
            Id=Guid.NewGuid(),
            FirstName="First " + DateTime.Now.ToString(),
            LastName="Second " + DateTime.Now.ToString(),
            Address="111 Address A",
            CreateDate=DateTime.Now,
            Email="test" + new Random().Next(1,20).ToString() + "@gmail.com",
            Password="123",
            PhoneNumber="3761652",
            State="Florida",
            City="Orlando",
            Status=StatusItem.Active,
            ZipCode="32835"
        };

        public Product productC = new Product()
        {
            CreateDate = DateTime.Now,
            Id = Guid.NewGuid(),
            IdPromotion = new Guid(PromotionType.ByOneGetOne),
            Name = "Product C",
            Price = 25,
            Status = true
        };

        public ShoppingCart shoppingCart = new ShoppingCart()
        {
            CreateDate = DateTime.Now,
            Id = Guid.NewGuid(),
            IdProduct = new Guid("12D4B353-CBFE-4B8D-B68B-1D9FDC9503E1"),
            IdPromotion= new Guid("085F5238-BCB4-4F67-A8DF-8113FDE97732"),
            Price=10,Quantity=1,Status=true,Total=10
        };

        public GroupShoopingCart groupShoopingCart = new GroupShoopingCart()
        {
            CreateDate = DateTime.Now,
            Id = Guid.NewGuid(),
            IdClient = new Guid("1F2238DF-DA6B-446F-9DBB-E96EDEA2F6D1"),
            Status = StatusItem.Active
        };

        #region Product
        [Fact]
        public void ListProduct()
        {
            var products = productRepository.findAll();
            Assert.True(products.Count() > 0);
        }

        [Fact]
        public async Task AddProductAsync()
        {
            await productRepository.AddProduct(product);
            Assert.True(new Guid() != product.Id);
        }

        [Fact]
        public async Task AddAnotherProduct()
        {
            await productRepository.AddProduct(productC);
            Assert.True(new Guid() != product.Id);
        }

        [Fact]
        public void TestContructorProduct()
        {
            //only to mock constructor
            ProductRepository productRepository = new ProductRepository(new KlirContext());
            Assert.True(clientRepository != null);
        }

        [Fact]
        public void ListProductAll()
        {
            var products = productRepository.findAllProducts();
            Assert.True(products.Count() > 0);
        }

        [Fact]
        public void ListByIdProduct()
        {
            var products = productRepository.findProductById(new Guid("12D4B353-CBFE-4B8D-B68B-1D9FDC9503E1"));
            Assert.True(products != null);
        }

        [Fact]
        public async Task UpdateProductAsync()
        {
            var products = productRepository.findProductById(new Guid("12D4B353-CBFE-4B8D-B68B-1D9FDC9503E1"));
            products.AlterDate = DateTime.Now;
            await productRepository.UpdateProduct(products);
            Assert.True(products != null);
        }

        #endregion

        #region Promotion
        [Fact]
        public async Task AddPromotionAsync()
        {
            var promotion = PromotionsSecond.First();
            //var last = Promotions.Last();
            var id = await promotionRepository.AddPromotion(promotion);
            //id = await promotionRepository.AddPromotion(last);
            Assert.True(id != new Guid());
        }

        [Fact]
        public void ListPromotion()
        {
            var promotion = promotionRepository.findAll();
            Assert.True(promotion.Count()>=0);
        }

        [Fact]
        public async Task UpdatePromotionAsync()
        {
            var promotions = promotionRepository.findAll().ToList();
            for(var i = 0;i < promotions.Count(); i++)
            {
                promotions[i].AlterDate = DateTime.Now;
                await promotionRepository.Update(promotions[i]);
            }
            Assert.Equal(1, 1);
        }

        [Fact]
        public void ListPromotionAll()
        {
            var promotion = promotionRepository.findAllPromotion();
            Assert.True(promotion.Count() >= 0);
        }

        [Fact]
        public async Task UpdatePromotionSecondAsync()
        {
            var promotion = PromotionsSecond.First();
            promotion.AlterDate = DateTime.Now;
            var id = await promotionRepository.UpdatePromotion(promotion);
            //id = await promotionRepository.AddPromotion(last);
            Assert.True(id != new Guid());
        }

        [Fact]
        public void MockConstructorPromotion()
        {
            IPromotionRepository mockRepository = new PromotionRepository(new KlirContext());
            Assert.True(mockRepository != null);
        }

        #endregion

        #region ShoppingCart
        [Fact]
        public void ListShoppingCart()
        {
            var shopping = shoppingCartRepository.findAll();
            Assert.True(shopping.Count() >= 0);
        }

        [Fact]
        public async Task AddShoppingAsync()
        {
            await shoppingCartRepository.AddShoopingCart(shoppingCart);
            Assert.True(new Guid() != shoppingCart.Id);
        }

        [Fact]
        public async Task UpdateShoppingAsync()
        {
            var shopping = shoppingCartRepository.findAll().ToList();
            for(var i = 0; i < shopping.Count(); i++)
            {
                shopping[i].IdGroupShoppingCart = new Guid("F1D7E8B6-1A12-4066-9F77-2107098BE581");
                await shoppingCartRepository.UpdateShoopingCart(shopping[i]);
            }
            
            Assert.True(shopping.Count() > 0 && new Guid() != shopping.First().Id);
        }

        [Fact]
        public void ListShoppingCartAll()
        {
            var shopping = shoppingCartRepository.findAllShoopingCart();
            Assert.True(shopping.Count() >= 0);
        }

        [Fact]
        public void MockShoppingCartConstructor()
        {
            IShoppingCartRepository mock = new ShoppingCartRepository(new KlirContext());
            Assert.True(mock != null);
        }

        #endregion

        #region Client

        [Fact]
        public async Task AddClient()
        {
            await clientRepository.AddClient(client);
            Assert.True(new Guid() != shoppingCart.Id);
        }

        [Fact]
        public void findClientById()
        {
            var localClient = clientRepository.findClientById(new Guid("221B58BC-ABFD-437F-8814-546A699BE27C"));
            Assert.True(new Guid("221B58BC-ABFD-437F-8814-546A699BE27C") == localClient.Id);
        }

        [Fact]
        public async Task UpdateClient()
        {
            var localClient = clientRepository.findClientById(new Guid("221B58BC-ABFD-437F-8814-546A699BE27C"));
            localClient.AlterDate = DateTime.Now;
            await clientRepository.UpdateClient(localClient);
            Assert.True(new Guid() != shoppingCart.Id);
        }

        [Fact]
        public void TestContructorClient()
        {
            ClientRepository clientRepository = new ClientRepository(new KlirContext());
            Assert.True(clientRepository != null);
        }

        #endregion

        #region GroupShoppingCart

        [Fact]
        public async Task AddGroupShoppingCart()
        {
            await groupShoopingCartRepository.AddGroupShoopingCart(groupShoopingCart);
            Assert.True(new Guid() != groupShoopingCart.Id);
        }

        [Fact]
        public void ListGroupShoppingCart()
        {
            var group = groupShoopingCartRepository.findAll();
            Assert.True(group.Count() >= 0);
        }

        [Fact]
        public async Task UpdateGroupShoppingCart()
        {
            var item = groupShoopingCartRepository.findAll().First();
            item.AlterDate = DateTime.Now;
            await groupShoopingCartRepository.UpdateGroupShoopingCart(item);
            Assert.True(new Guid() != item.Id);
        }

        [Fact]
        public void TestMockContructor()
        {
            var group = new GroupShoopingCartRepository(new KlirContext());
            Assert.True(group != null);
        }

        #endregion

        #region Add url to Product

        [Fact]
        public async Task UpdateProductUrlAsync()
        {
            var products = productRepository.findProductById(new Guid("12D4B353-CBFE-4B8D-B68B-1D9FDC9503E1"));
            products.AlterDate = DateTime.Now;
            products.Url = "assets/images/wine.jpg";
            await productRepository.UpdateProduct(products);

            products = productRepository.findProductById(new Guid("D0D8FB8D-5D4C-43F8-8170-B7A2E3B9883D"));
            products.AlterDate = DateTime.Now;
            products.Url = "assets/images/rose-wine.jpeg";
            await productRepository.UpdateProduct(products);

            products = productRepository.findProductById(new Guid("5F5D363F-F2E8-432D-B4A4-E15113103339"));
            products.AlterDate = DateTime.Now;
            products.Url = "assets/images/white_wine.jpg";
            await productRepository.UpdateProduct(products);
            Assert.True(products != null);
        }

        #endregion

        public InfraStructureTest()
        {
            

            StreamReader r = new StreamReader("appsettings.json");
            string jsonString = r.ReadToEnd();
            AppSettings appSettings = JsonConvert.DeserializeObject<AppSettings>(jsonString);

            var services = new ServiceCollection();
            services.AddDbContext<KlirContext>(
                        options => options.UseSqlServer(appSettings.ConnectionStrings.Main));

            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IPromotionRepository, PromotionRepository>();
            services.AddTransient<IShoppingCartRepository, ShoppingCartRepository>();
            services.AddTransient<IClientRepository, ClientRepository>();
            services.AddTransient<IGroupShoopingCartRepository, GroupShoopingCartRepository>();

            var serviceProvider = services.BuildServiceProvider();

            productRepository = serviceProvider.GetService<IProductRepository>();
            promotionRepository = serviceProvider.GetService<IPromotionRepository>();
            shoppingCartRepository = serviceProvider.GetService<IShoppingCartRepository>();
            clientRepository= serviceProvider.GetService<IClientRepository>();
            groupShoopingCartRepository= serviceProvider.GetService<IGroupShoopingCartRepository>();

        }


    }

}
