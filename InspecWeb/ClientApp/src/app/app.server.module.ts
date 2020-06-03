import { NgModule } from '@angular/core';
import { ServerModule } from '@angular/platform-server';
import { ModuleMapLoaderModule } from '@nguniversal/module-map-ngfactory-loader';
import { AppComponent } from './app.component';
import { AppModule } from './app.module';
import { ExternalOrganizationModule } from './external-organization/external-organization.module';

@NgModule({
    imports: [AppModule, ServerModule, ModuleMapLoaderModule,ExternalOrganizationModule],
    bootstrap: [AppComponent]
})
export class AppServerModule { }
