import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
//import { Guid } from 'guid-typescript';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
})
export class LoginComponent {

  public http: HttpClient;
  public baseUrl:string;
  public name:string='';
  public password:string='';
  public user:ClientUser;

  constructor(http: HttpClient, @Inject('BASE_API_URL') baseUrl: string, route: ActivatedRoute
   ) {
    //this.id = route.snapshot.paramMap.get('id');
    this.http=http;
    this.baseUrl=baseUrl;
    sessionStorage.setItem('client', null);
    
  }

  authenticate(){
    if(this.name == '' || this.password==''){
      alert('Please type the e-mail and password in order to continue!');
      return;
    }

    this.http.get<ClientUser>(this.baseUrl + 'client/authenticate/' + this.name + '/' + this.password ).subscribe(result => {
      this.user = result;
      if(this.user == null){
         var okay = confirm('E-mail or password not exists, do you would like to create a new one ?');
         if(okay==true){
            this.user = {
              Id:null,
              Email:this.name,
              Password:this.password,
              FirstName:'',
              LastName:'',
              Address:'',
              State:'',
              City:'',
              ZipCode:'',
              PhoneNumber:''
            };
            this.createNewUser(this.user)
            .subscribe(data => {
              console.log(data);
              this.authenticate();
            }); 
         }
      }else{
        sessionStorage.setItem('client', JSON.stringify(this.user));
        window.location.href='/shop';
      }
      console.log(this.user);
    }, error => console.error(error));
  }

  createNewUser(newUser:ClientUser): Observable<any> {
    const headers = { 'content-type': 'application/json'}  
    const body=JSON.stringify(newUser);
    console.log(body)
    return this.http.post(this.baseUrl + 'client', body,{'headers':headers});
  }
}

interface ClientUser {
  Id:string,
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