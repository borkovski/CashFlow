import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { UniversalModule } from 'angular2-universal';
import { AppComponent } from './components/app/app.component'
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { TransferListComponent } from './components/transfer/transferList.component';
import { TransferComponent } from './components/transfer/transfer.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TransferService } from './services/transfer/transfer.service';

@NgModule({
    bootstrap: [ AppComponent ],
    declarations: [
        AppComponent,
        NavMenuComponent,
        HomeComponent,
        TransferListComponent,
        TransferComponent
    ],
    providers: [TransferService],
    imports: [
        UniversalModule, // Must be first import. This automatically imports BrowserModule, HttpModule, and JsonpModule too.
        FormsModule,
        ReactiveFormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'transferList', component: TransferListComponent },
            { path: 'transfer', component: TransferComponent },
            { path: '**', redirectTo: 'home' }
        ])
    ]
})
export class AppModule {
}
