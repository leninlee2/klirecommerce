import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
//import { Guid } from 'guid-typescript';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-productitem',
  templateUrl: './productitem.component.html',
})
export class ProductItemComponent {
  public product: ProductItem=new ProductItem();
  public productfeatures: ProductFeature[];
  public shopingCart:ShopingCart;
  public id:string;
  public qtde:number=1;
  public http: HttpClient;
  public baseUrl:string;
  public user:ClientUser;

  constructor(http: HttpClient, @Inject('BASE_API_URL') baseUrl: string, route: ActivatedRoute) {
    this.id = route.snapshot.paramMap.get('id');
    this.http=http;
    this.baseUrl=baseUrl;

    this.user = JSON.parse(sessionStorage.getItem("client")) as ClientUser;
    if(this.user==null){
      alert('In Order to buy something you need to be authenticated');
      window.location.href='/login';
    }
  }

  ngOnInit() {
    this.http.get<ProductItem>(this.baseUrl + 'product/byid/' + this.id).subscribe(result => {
      this.product = result;

      console.log(this.product);
    }, error => console.error(error));

    //get product features:
    this.http.get<ProductFeature[]>(this.baseUrl + 'product').subscribe(result => {
      this.productfeatures = result;
    }, error => console.error(error));
  }

  public addCart() {
    this.shopingCart = {
      IdProduct:this.id,
      Quantity:this.qtde,
      IdClient:this.user.id
    };
    this.addShoppingCart(this.shopingCart)
    .subscribe(data => {
      console.log(data)
      //alert('Product was add to your cart');
      var okay = confirm('Product was add to your cart! Do you want finish your order ? ');
      if(okay==true)
        window.location.href='/cart';
      else 
        window.location.href='/shop';
    });      
  }

  addShoppingCart(shopingCart:ShopingCart): Observable<any> {
    const headers = { 'content-type': 'application/json'}  
    const body=JSON.stringify(shopingCart);
    console.log(body)
    return this.http.post(this.baseUrl + 'shoppingcart', body,{'headers':headers});
  }

}

class ProductItem {
  Id: string;
  Name: string;
  Price: number;
}

interface ProductFeature {
  Id: string;
  Name: string;
}

interface ShopingCart {
  IdProduct:string;
  Quantity:number;
  IdClient:string;
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

