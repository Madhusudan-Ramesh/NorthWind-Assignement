import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-Advertisement',
  templateUrl: './Advertisement.component.html',
  styleUrls: ['./Advertisement.component.css']
})
export class AdvertisementComponent implements OnInit {

  @Input() data='';
  constructor() { }

  ngOnInit() {
  }

}
