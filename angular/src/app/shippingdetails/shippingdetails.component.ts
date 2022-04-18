import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { Guid } from 'guid-typescript';
import { Observable } from 'rxjs';
//import { InputMaskAngularModule } from 'input-mask-angular';

@Component({
  selector: 'app-shippingdetails',
  templateUrl: './shippingdetails.component.html',
})
export class ShippingDetailsComponent {
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
    public firstName:string;
    public lastName:string;
    public address:string;
    public state:string;
    public city:string;
    public zipcode:string;
    public phonenumber:string;
  
    public invalidChars = [
      "0",
      "1",
      "2",
      "3",
      "4",
      "5",
      "6",
      "7",
      "8",
      "9",
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

      //case authenticated:
      //alert(this.user.id);
      this.firstName = this.user.firstName;
      this.lastName = this.user.lastName;
      this.address = this.user.address;
      this.state = this.user.state;
      this.city = this.user.city;
      this.zipcode = this.user.zipCode;
      this.phonenumber = this.user.phoneNumber;

    }
  
    ngOnInit() {
  
      //get product features:
      this.http.get<ShopingCarts[]>(this.baseUrl + 'shoppingcart/' + this.user.id).subscribe(result => {
        this.shoppingCarts = result;
        console.log(this.shoppingCarts);
        this.groupByProduct();
      }, error => console.error(error));

      this.http.get<ClientUser>(this.baseUrl + 'client/' + this.user.id).subscribe(result => {
        //this.shoppingCarts = result;
        //console.log(this.shoppingCarts);
        //this.groupByProduct();
        this.firstName = result.firstName;
        this.lastName = result.lastName;
        this.address = result.address;
        this.state = result.state;
        this.city = result.city;
        this.zipcode = result.zipCode;
        this.phonenumber = result.phoneNumber;

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
  
    updateShipping(user:ClientUser): Observable<any> {
      const headers = { 'content-type': 'application/json'}  
      const body=JSON.stringify(user);
      console.log(body)
      return this.http.post(this.baseUrl + 'client/update', body,{'headers':headers});
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
    

    saveShipping(){
        //alert('Test');
      this.user.firstName=this.firstName;
      this.user.lastName=this.lastName;
      this.user.address=this.address;
      this.user.state=this.state;
      this.user.city=this.city;
      this.user.zipCode=this.zipcode;
      this.user.phoneNumber=this.phonenumber;

      this.updateShipping(this.user)
      .subscribe(data => {
        console.log(data);
        window.location.href='/payment';
      }); 
    }

    onKeyDownEvent(e: any){
      console.log(e.target.value);
      if (!this.invalidChars.includes(e.key)) {
        e.preventDefault();
      }
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
    email:string,
    password:string,
    firstName:string,
    lastName:string,
    address:string,
    state:string,
    city:string,
    zipCode:string,
    phoneNumber:string
  }