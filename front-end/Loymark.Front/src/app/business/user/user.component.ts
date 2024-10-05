import { CommonModule } from '@angular/common';
import { Component, OnInit, signal } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { UserService } from '../../services/user.service';

export interface User {
  id: number;
  number: number;
  name: string;
  lastName: string;
  email: string;
  countryCode: string;
  birthDay: string;
  receiveInformation: boolean;
}

export interface UserToRegister {
  number: number;
  name: string;
  lastName: string;
  email: string;
  countryCode: string;
  birthDay: string;
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
  public countries = [
    { code: 'CRI', name: 'Costa Rica' },
    { code: 'USA', name: 'Estados Unidos' },
    { code: 'MEX', name: 'México' },
    { code: 'CAN', name: 'Canadá' },
    { code: 'ESP', name: 'España' },
    { code: 'COL', name: 'Colombia' },
  ];
  public users: User[] = [];
  public isModalOpen = signal(false);

  constructor(private userService: UserService){ }

  ngOnInit(): void {
    this.getUsers();
    this.isModalOpen.set(false);
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
          alert("Ocurrió un error al ingresar la información");
        }
      });
    }
  }

  openModal(){
    this.isModalOpen.set(true);
  }

  closeModal(){
    this.isModalOpen.set(false);
  }

  closeModalOnOutSideClick(event: MouseEvent){
    const targetElement = event.target as HTMLElement;
    if (targetElement.classList.contains('fixed')){
      this.closeModal();
    }
  }

  private getUsers(){
    this.userService.getUsers().subscribe(
      (response: User[]) => {
        this.users = response;
      },
      (error) => {
        console.error('Error fetching users', error);
      }
    );
  }
}
