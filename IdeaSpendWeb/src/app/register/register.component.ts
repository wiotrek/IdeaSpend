import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  // Model for register properties
  registerModel: any = {};

  // TODO: Make localization for images url
  logoImage = '../assets/logo/Logo.png';

  constructor(private router: Router, private authService: AuthService) { }

  ngOnInit(): void {
  }

  // After successful register user redirect him to login page
  register(): void {
    this.authService.register(this.registerModel).subscribe(

      async () => {

        console.log('Instead this log show user success message');
        await this.delay(3000);
        await this.router.navigate(['/login']);

      },

      error => {
        console.log('Instead this log show user what was wrong while registration');
      });

  }

  // Taking a specific time in milliseconds before execute next action
  async delay(ms: number): Promise<unknown> {
    return new Promise( resolve => setTimeout(resolve, ms) );
  }
}
