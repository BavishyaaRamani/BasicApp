import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { EmployeeService } from '../service/employee.service';
import { Employee } from './models/employee';
import { ReactiveFormsModule,FormBuilder, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [[CommonModule],[RouterOutlet],[ReactiveFormsModule]],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {

  empArr : Employee[] = [];

  empFormGroup : FormGroup;

  constructor(private empService : EmployeeService, private fb : FormBuilder) {
    this.empFormGroup = this.fb.group({
      id : [""],
      name : [""],
      mobileNo : [""],
      emailID : [""],
    })
  }

  ngOnInit(): void {
    this.getEmployees();
  }

  getEmployees(){

    this.empService.GetEmployee().subscribe(response =>
      {
        this.empArr = response;
      }
      )
  }

  OnSubmit(){
    if(this.empFormGroup.value.id != null && this.empFormGroup.value.id != "")
    {
      this.empService.UpdateEmployee(this.empFormGroup.value).subscribe(response => 
        {
          this.getEmployees();
          this.empFormGroup.setValue({
            id: "",
            name: "",
            mobileNo: "",
            emailID: "",
          })
        })
    }
    else{
      this.empService.CreateEmployee(this.empFormGroup.value).subscribe(response =>
        {
          this.getEmployees();
          this.empFormGroup.setValue({
            id : "",
            name : "",
            mobileNo : "",
            emailID : "",
          })
        })
      }
  }

  Fillform(emp:Employee){
      this.empFormGroup.setValue({
        id: emp.id,
        name: emp.name,
        mobileNo: emp.mobileNo,
        emailID: emp.emailID,
      })
  }

  DeleteEmp(id:string){
    this.empService.DeleteEmployee(id).subscribe(response => 
    {
      this.getEmployees();
    }
    )
  }

  title = 'basicapp';
}
