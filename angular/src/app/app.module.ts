import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { AboutComponent } from './about/about.component';
import { ShopComponent } from './shop/shop.component';
import { CartComponent } from './cart/cart.component';
import { ProductItemComponent } from './productitem/productitem.component';
import { LoginComponent } from './login/login.component';
import { ShippingDetailsComponent } from './shippingdetails/shippingdetails.component';
import { PaymentComponent } from './payment/payment.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    AboutComponent,
    ShopComponent,
    CartComponent,
    ProductItemComponent,
    LoginComponent,
    ShippingDetailsComponent,
    PaymentComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'about', component: AboutComponent },
      { path: 'shop', component: ShopComponent },
      { path: 'cart', component: CartComponent },
      { path: 'productitem/:id', component: ProductItemComponent },
      { path: 'login', component: LoginComponent },
      { path: 'shippingdetails', component: ShippingDetailsComponent },
      { path: 'payment', component: PaymentComponent }
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
