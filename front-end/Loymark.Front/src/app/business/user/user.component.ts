import { CommonModule, formatDate } from '@angular/common';
import { Component, OnInit, signal, ViewChild } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { UserService } from '../../services/user.service';

export interface User {
  id: number;
  number: number;
  name: string;
  lastName: string;
  email: string;
  countryCode: string;
  birthDay: Date;
  receiveInformation: boolean;
}

export interface UserToRegister {
  number: number;
  name: string;
  lastName: string;
  email: string;
  countryCode: string;
  birthDay: Date;
  receiveInformation: boolean;
}

@Component({
  selector: 'app-user',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './user.component.html',
  styleUrl: './user.component.css'
})

export default class UserComponent implements OnInit{
  @ViewChild('userEditForm') userEditForm!: NgForm;
  @ViewChild('userForm') userForm!: NgForm;
  
  public countries = [
    { code: 'CRI', name: 'Costa Rica' },
    { code: 'USA', name: 'Estados Unidos' },
    { code: 'MEX', name: 'México' },
    { code: 'CAN', name: 'Canadá' },
    { code: 'ESP', name: 'España' },
    { code: 'COL', name: 'Colombia' },
  ];
  public users: User[] = [];
  public user: User = {
    id: 0,
    number: 0,
    name: '',
    lastName: '',
    email: '',
    countryCode: '',
    birthDay: new Date(),
    receiveInformation: false
  };
  public isModalOpen = signal(false);
  public isModalEditOpen = signal(false);
  public date: string = new Date().toISOString().slice(0, 10);

  constructor(private userService: UserService){ }

  ngOnInit(): void {
    this.getUsers();
    this.isModalOpen.set(false);
    this.isModalEditOpen.set(false);
  }

  addUser(form: NgForm){
    if (form.valid) {
      const user: UserToRegister = {
        name: form.value.name,
        lastName: form.value.lastName,
        email: form.value.email,
        birthDay: form.value.birthDay,
        number: form.value.number,
        countryCode: form.value.countryCode,
        receiveInformation: form.value.receiveInformation === 'true'
      };

      this.userService.registerUser(user).subscribe((res) => {
        if (res != null){
          this.getUsers();
          this.closeModal();
        }else{
           window.alert("Ocurrió un error al ingresar la información.");
        }
      }, (error) => window.alert(`Ocurrió un error al ingresar la información - ${error.message}`) );
    }
  }

  updateUser(form: NgForm){
    if (form.valid){
      const user: User = {
        id: this.user.id,
        name: form.value.name,
        lastName: form.value.lastName,
        email: form.value.email,
        birthDay: form.value.birthDay,
        number: form.value.number,
        countryCode: form.value.countryCode,
        receiveInformation: form.value.receiveInformation === 'true'
      };

      this.userService.updateUser(user).subscribe((res) => {
        if (res != null){
          this.getUsers();
          this.closeModal();
        }else{
           window.alert("Ocurrió un error al ingresar la información.");
        }
      }, (error) => window.alert(`Ocurrió un error al ingresar la información - ${error.message}`) );
    }
  }

  deleteUser(id: number){
    this.userService.deleteUser(id).subscribe(res => {
      if (res.status == 200){
        this.getUsers();
      }else{
         window.alert("Ocurrió un error al intentar borrar el usuario.");
      }
      console.log(res);
    }, (error) => window.alert(`Ocurrió un error al intentar borrar el usuario - ${error.message}`) );
  }

  openModal(){
    this.resetForm();
    this.isModalOpen.set(true);
  }

  openEditModal(user: User){
    this.resetForm();
    this.user = user;
    this.isModalEditOpen.set(true);
  }

  closeModal(){
    this.isModalOpen.set(false);
    this.isModalEditOpen.set(false);
    this.resetForm();
    this.getUsers();
  }

  closeModalOnOutSideClick(event: MouseEvent){
    const targetElement = event.target as HTMLElement;
    if (targetElement.classList.contains('fixed')){
      this.closeModal();
      this.getUsers();
    }    
  }

  private resetForm(){
    this.userForm.resetForm();
    this.userEditForm.resetForm();
  }

  private getUsers(){
    this.userService.getUsers().subscribe(
      (response: User[]) => {
        this.users = response;
      }, (error) => window.alert(`Ocurrió un error al intentar obtener los usuarios - ${error.message}`) );
  }
}
