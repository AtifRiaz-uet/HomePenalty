import { Component } from '@angular/core';
import { ConnectionService } from '../Services/connection.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { map } from 'rxjs/operators';
import { DatePipe } from '@angular/common';
import { Injectable } from '@angular/core';
@Injectable({
   providedIn: 'root',
})

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
    countriesList: string[];
    PenaltyForm: FormGroup;
    days:number;
    cIN:any;
    chcIn:string;
    chcOu:string;
    cOU:any;
    constructor(private interactionService: ConnectionService, private datePipe: DatePipe) { }

    ngOnInit(): void {
        this.interactionService.getCountriesName()
            .pipe(map((data: string[]) => {
                this.countriesList = data;
            })
            )
            .subscribe();
        this.PenaltyForm = new FormGroup({
            country: new FormControl('', Validators.required),
            checkIn: new FormControl('', Validators.required),
            checkOut: new FormControl('', Validators.required)

        });
    }

    onSubmit()
    {
        console.log("Button Pressed!");
        if (this.PenaltyForm.valid) {
            this.cIN=this.PenaltyForm.value.checkIn;
            this.cOU=this.PenaltyForm.value.checkOut;
            if (this.cOU >= this.cIN)
            {
                this.interactionService.postDates(this.cIN,this.cOU)
                .pipe(map((day: number) => {
                this.days = day;
                })
                )
                .subscribe();
                console.log("Form Submitted");

            }
            else
            {
                alert('There is Some Error')
            }
            
            console.log(this.PenaltyForm);

        }
        else {
            console.log(this.PenaltyForm);
            alert('There is Something Missing!');

        }
        
    }
}
