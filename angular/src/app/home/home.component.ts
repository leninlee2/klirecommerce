import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  public productfeatures: ProductFeature[];
  constructor(http: HttpClient, @Inject('BASE_API_URL') baseUrl: string) {
    http.get<ProductFeature[]>(baseUrl + 'product').subscribe(result => {
      this.productfeatures = result;
    }, error => console.error(error));
  }
}

interface ProductFeature {
  Id: string;
  Name: string;
}