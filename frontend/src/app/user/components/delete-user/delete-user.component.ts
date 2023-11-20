import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { first } from 'rxjs';
import { UserId } from '../../models/models';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-delete-user',
  templateUrl: './delete-user.component.html',
  styleUrls: ['./delete-user.component.css']
})
export class DeleteUserComponent implements OnInit {

  users!: UserId[];

  constructor(private _router: Router, private _userService: UserService) { }



  ngOnInit(): void {
    this._userService.getAllUsers().pipe(first()).subscribe((response) => {
      this.users = response;
    });
  }

  deleteUser(id: string) {
    this._userService.deleteUser(id).subscribe();
    this._router.navigate(['user'])
  }

  goBack() {
    this._router.navigate(['user']);
  }

}
