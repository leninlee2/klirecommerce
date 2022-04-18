import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { Guid } from 'guid-typescript';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
})
export class CartComponent {
  public shoppingCarts: ShopingCarts[];
  public shoppingCartItem: ShopingCartItem;
  public shoppingCartItemUpdate: ShopingCartItemUpdate;
  public summarize: Summarization[] = [];
  public productList:string[] = [];
  public http: HttpClient;
  public baseUrl:string;
  public total:number=0;
  public buyOneGetOne:string;
  public buyThreeForTen:string;
  public user:ClientUser;

  public invalidChars = [
    "-",
    "+",
    "e",
  ];

  constructor(http: HttpClient, @Inject('BASE_API_URL') baseUrl: string, route: ActivatedRoute,
    @Inject('buyOneGetOne') buyOneGetOne: string,
    @Inject('buyThreeForTen') buyThreeForTen: string
   ) {
    //this.id = route.snapshot.paramMap.get('id');
    this.http=http;
    this.baseUrl=baseUrl;
    this.buyOneGetOne=buyOneGetOne;
    this.buyThreeForTen=buyThreeForTen;

    this.user = JSON.parse(sessionStorage.getItem("client")) as ClientUser;
    if(this.user==null){
      alert('In Order to buy something you need to be authenticated');
      window.location.href='/login';
    }
  }

  ngOnInit() {

    //get product features:
    this.http.get<ShopingCarts[]>(this.baseUrl + 'shoppingcart/' + this.user.id).subscribe(result => {
      this.shoppingCarts = result;
      console.log(this.shoppingCarts);
      this.groupByProduct();
    }, error => console.error(error));
  }

  groupByProduct(){
    let counter = -1;
    //alert(JSON.stringify(this.shoppingCarts));
    for(var i = 0;i < this.shoppingCarts.length;i++){
      if(this.productList.length==0 || this.productList.indexOf(this.shoppingCarts[i].product.id) < 0){
        counter=counter+1;
        this.productList[this.productList.length]= this.shoppingCarts[i].product.id;
        this.summarize[counter] = {IdProduct:this.shoppingCarts[i].product.id
          ,Name:this.shoppingCarts[i].product.name,Value:0,Quantity:0
          ,Price:this.shoppingCarts[i].product.price
          ,IdPromotion:this.shoppingCarts[i].product.idPromotion
          ,Detail:''
        };
        for(var j = 0;j < this.shoppingCarts.length;j++){
          if(this.summarize[counter].IdProduct == this.shoppingCarts[j].product.id){
            //this.summarize[counter].Value+=(this.shoppingCarts[j].product.price*this.shoppingCarts[j].shoppingCart.quantity);
            this.summarize[counter].Quantity+=this.shoppingCarts[j].shoppingCart.quantity;
          }
        }
      }
    }
    //apply promotions:
    this.summarize=this.applicatePromotionRule(this.summarize);
    //total:
    for(var i = 0;i < this.summarize.length;i++){
      this.total += this.summarize[i].Value;
    }

  }

  cancel(){
    window.location.href='/shop';
  }

  removeItem(id:string){
    //alert(id);
    this.shoppingCartItem = {
      Id:id
    };
    this.removeShoppingCart(this.shoppingCartItem)
    .subscribe(data => {
      console.log(data)
      alert('Product was removed to your cart! ');
      window.location.reload();
    });  
  }

  removeShoppingCart(shopingCart:ShopingCartItem): Observable<any> {
    const headers = { 'content-type': 'application/json'}  
    const body=JSON.stringify(shopingCart);
    console.log(body)
    return this.http.post(this.baseUrl + 'shoppingcart/inactive', body,{'headers':headers});
  }

  updateShoppingCart(shopingCart:ShopingCartItemUpdate): Observable<any> {
    const headers = { 'content-type': 'application/json'}  
    const body=JSON.stringify(shopingCart);
    console.log(body)
    return this.http.post(this.baseUrl + 'shoppingcart/update', body,{'headers':headers});
  }

  onKeyDownEvent(e: any){
    console.log(e.target.value);
    if (this.invalidChars.includes(e.key)) {
      e.preventDefault();
    }
  }

  onChange(e: any,id:string){
    console.log(e.target.value);
    if(e.target.value==''){
      e.target.value='1';
    }

    this.shoppingCartItemUpdate = {
      Id:id,
      Quantity:e.target.value*1
    };
    this.updateShoppingCart(this.shoppingCartItemUpdate)
    .subscribe(data => {
      console.log(data);
      window.location.reload();
    });  

  }

  applicatePromotionRule(entry){
    for(var i = 0;i < entry.length;i++){
      var promotion = entry[i].IdPromotion.replace('"','').replace('"','').toString();
      if(entry[i].Quantity <=1 || (promotion.toUpperCase() == this.buyThreeForTen.toUpperCase() && entry[i].Quantity < 3 ) ){
        entry[i].Value=entry[i].Quantity*entry[i].Price;
      }else if(promotion.toUpperCase() == this.buyOneGetOne.toUpperCase()){
        var quotient = Math.floor(entry[i].Quantity/2);
        var remainder = entry[i].Quantity % 2;
        var value = (quotient*entry[i].Price) + (remainder*entry[i].Price);
        entry[i].Value=value;
        entry[i].Detail='Applied promotion Buy one Get one';
      }else if(promotion.toUpperCase() == this.buyThreeForTen.toUpperCase() && entry[i].Quantity >=3){
        var quotient = Math.floor(entry[i].Quantity/3);
        var remainder = entry[i].Quantity % 3;
        var value = (quotient*10) + (remainder*entry[i].Price);
        entry[i].Detail='Applied promotion 3 for ten';
        entry[i].Value=value;
      }
    }
    return entry;
  }
  
  redirectShipping(){
    window.location.href='/shippingdetails';
  }

}

interface ShopingCarts {
  product:Product;
  shoppingCart:ShopingCart;
  promotion:Promotion;
}

interface Promotion{
  Id:string;
  Name:string;
}

interface ShopingCart {
  Name:string;
  price:number;
  quantity:number;
}

interface ShopingCartItem {
  Id:any;
}

interface ShopingCartItemUpdate {
  Id:any;
  Quantity:number;
}

interface Product {
  id:string;
  name:string;
  price:number;
  idPromotion:string;
}

interface Summarization {
  IdProduct:string;
  Name:string;
  Value:number;
  Price:number;
  Quantity:number;
  IdPromotion:string;
  Detail:string;
}

interface ClientUser {
  id:string,
  Email:string,
  Password:string,
  FirstName:string,
  LastName:string,
  Address:string,
  State:string,
  City:string,
  ZipCode:string,
  PhoneNumber:string
}