import { NgModule, provideBrowserGlobalErrorListeners } from '@angular/core';
import { BrowserModule, provideClientHydration, withEventReplay } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing-module';
import { App } from './app';
import { CoreModule } from './core/core-module';
import {provideHttpClient, withFetch } from '@angular/common/http';
import { HomeModule } from "./home/home-module";
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';








@NgModule({
  declarations: [
    App
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    CoreModule,
    HomeModule,
    BrowserAnimationsModule,
ToastrModule.forRoot({
  timeOut: 3000,               // ⏱ مدة ظهور الرسالة (بالملي ثانية)
  positionClass: 'toast-top-right', // 📍 مكان ظهور التوست
  preventDuplicates: true,     // 🚫 يمنع تكرار نفس الرسالة
  closeButton: true,           // ❌ زر إغلاق يدوي
  progressBar: true,           // 🔵 شريط تقدم للوقت
  progressAnimation: 'decreasing', // 🔄 شكل حركة الشريط
  newestOnTop: true,           // 🆕 يظهر أحدث توست فوق الباقي
  tapToDismiss: true,          // 👆 الضغط عليه يخفيه
  easeTime: 300,               // ⏳ مدة الأنيميشن
  enableHtml: true,            // 🧩 يدعم HTML داخل الرسالة
}),
  
  ],
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideClientHydration(withEventReplay()),
    provideHttpClient(withFetch()),


  ],
  bootstrap: [App]
})
export class AppModule {

}
