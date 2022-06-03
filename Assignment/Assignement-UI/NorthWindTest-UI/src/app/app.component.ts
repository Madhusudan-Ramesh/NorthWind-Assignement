

import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { map } from 'rxjs';
import { AppService } from './app.service';
import { Token } from './Components/Model/TokenKey';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'NorthWindTest-UI';
  AdvertisementData = 'Welcome To Employees Sales Record'
  userFormVisible: boolean = true;
  salesDataVisible: boolean = false;

  employeesSales: any[]=[]
  page: number = 1;
  count: number = 0;
  tableSize: number = 8;
  inValidData: boolean = false;

  

  constructor(private service:AppService) { }

  userData = new FormGroup({
    Name : new FormControl('',[Validators.required]),
    password : new FormControl('',[Validators.required])
  })
  
  ngOnInit() {
    this.getEmployeesSales()
  }

  public getEmployeesSales(): void{
    this.service.getEmployeesSales()
    .subscribe((data:any[]) =>{
      this.employeesSales = data,
      sessionStorage.setItem('empData', <any>data)
    })
  }

  onTableDataChange(event: any) {
    this.page = event;
    this.getEmployeesSales();
  }

  onTableSizeChange(event: any): void {
    this.tableSize = event.target.value;
    this.page = 1;
  }

  get Validation(){
    
    let tokenKey = sessionStorage.getItem('token')
    if(tokenKey==null){
      return this.userFormVisible
    }
    return false
  }

  get tabelValidation(){
    let tokenKey = sessionStorage.getItem('token')
    if(tokenKey){
      return this.salesDataVisible = true
    }
    return false
  }

  onSubmit(){
    debugger;
    this.service.getToken(this.userData.value).
    subscribe({
      next: (res:Token)=>{
        console.log(res)
        let token = res.token;
        sessionStorage.setItem('token', token)
        this.getEmployeesSales()
      },
      error: err=>{
        console.log(err)
        this.inValidData = true;
      }
    })
    
  }

  onLogout(){
    sessionStorage.removeItem('token');
    this.tabelValidation
  }
}
