import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

//Decorators => a way to giving a normal class more some extra power and 
//in this case is giving our class to be an angular component
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

//OnInit : event that takes place after the constractor is known as the initialization.
export class AppComponent implements OnInit {
  title = 'The Dating App';//property
  users: any;
  //any : turning off type safety in type script
  //ممكن للمستخدمين ان يكونوا سترنج او ارقام او مصفوفة => any type for users property 

  constructor(private http: HttpClient) { }//dependency injection

  ngOnInit() {
    //throw new Error('Method not implemented.');
    this.GetUsers();
  }

  GetUsers() {
    this.http.get('https://localhost:5001/api/users').subscribe(response => {
      this.users = response;
    }, error => {
      console.log(error);
    })
  }
}
