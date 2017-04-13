import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpModule, JsonpModule } from '@angular/http';
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
import { TransferPendingListComponent } from './components/transferSchema/transferPendingList.component';

import { AccountService } from './services/account/account.service';
import { TransferService } from './services/transfer/transfer.service';
import { TransferSchemaService } from './services/transferSchema/transferSchema.service';
import { TransferPendingService } from './services/transferSchema/transferPending.service';
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
        TransferSchemaComponent,
        TransferPendingListComponent
    ],
    providers: [TransferService, AccountService, TransferSchemaService, TransferPeriodService, TransferPendingService],
    imports: [
        BrowserModule.withServerTransition({
            appId: 'cashFlow'
        }),
        BrowserAnimationsModule,
        HttpModule,
        JsonpModule,
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
