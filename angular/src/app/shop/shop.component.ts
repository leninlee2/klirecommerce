import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
})
export class ShopComponent {
  public products: Product[];
  constructor(http: HttpClient, @Inject('BASE_API_URL') baseUrl: string) {
    http.get<Product[]>(baseUrl + 'product').subscribe(result => {
      this.products = result;
    }, error => console.error(error));
  }

  redirectProduct(id:string){
    //alert(id);
    window.location.href='/productitem/' + id;
  }
}

interface Product {
  Id: string;
  Name: string;
}