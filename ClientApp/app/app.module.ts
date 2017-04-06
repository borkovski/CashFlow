import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { UniversalModule } from 'angular2-universal';
import { AppComponent } from './components/app/app.component'
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { HomeComponent } from './components/home/home.component';
import { AccountListComponent } from './components/account/accountList.component';
import { AccountComponent } from './components/account/account.component';
import { AccountHistoryComponent } from './components/account/accountHistory.component';
import { AccountHistoryChartComponent } from './components/chart/accountHistoryChart.component';
import { TransferListComponent } from './components/transfer/transferList.component';
import { TransferComponent } from './components/transfer/transfer.component';
import { TransferSchemaListComponent } from './components/transferSchema/transferSchemaList.component';
import { TransferSchemaComponent } from './components/transferSchema/transferSchema.component';

import { AccountService } from './services/account/account.service';
import { TransferService } from './services/transfer/transfer.service';
import { TransferSchemaService } from './services/transferSchema/transferSchema.service';
import { TransferPeriodService } from './services/transferSchema/transferPeriod.service';

@NgModule({
    bootstrap: [ AppComponent ],
    declarations: [
        AppComponent,
        NavMenuComponent,
        HomeComponent,
        AccountListComponent,
        AccountComponent,
        AccountHistoryComponent,
        AccountHistoryChartComponent,
        TransferListComponent,
        TransferComponent,
        TransferSchemaListComponent,
        TransferSchemaComponent
    ],
    providers: [TransferService, AccountService, TransferSchemaService, TransferPeriodService],
    imports: [
        UniversalModule, // Must be first import. This automatically imports BrowserModule, HttpModule, and JsonpModule too.
        FormsModule,
        ReactiveFormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'accountList', component: AccountListComponent },
            { path: 'account', component: AccountComponent },
            { path: 'accountHistory', component: AccountHistoryComponent },
            { path: 'transferList', component: TransferListComponent },
            { path: 'transfer', component: TransferComponent },
            { path: 'transferSchemaList', component: TransferSchemaListComponent },
            { path: 'transferSchema', component: TransferSchemaComponent },
            { path: '**', redirectTo: 'home' }
        ])
    ]
})
export class AppModule {
}
