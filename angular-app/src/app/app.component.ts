import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { PedidosListComponent } from './pedidos/pedidos-list/pedidos-list.component';

/**
 * Shell mínimo para ejecutar el módulo de Pedidos de forma aislada.
 * En un sistema real, este módulo viviría dentro de tu aplicación Angular
 * (con su router, su layout y su login).
 */
@Component({
  selector: 'app-root',
  standalone: true,
  imports: [FormsModule, PedidosListComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  usuario = 'supervisor';
  password = '';
  autenticado = !!localStorage.getItem('token');
  mensaje = '';

  clienteIdEntrada = 1;
  clienteIdActivo: number | null = null;

  constructor(private http: HttpClient) {}

  iniciarSesion(): void {
    this.http
      .post<{ token: string }>('/api/auth/login', { usuario: this.usuario, password: this.password })
      .subscribe({
        next: ({ token }) => {
          localStorage.setItem('token', token);
          this.autenticado = true;
          this.mensaje = '';
        },
        error: () => (this.mensaje = 'No se pudo iniciar sesión.')
      });
  }

  cerrarSesion(): void {
    localStorage.removeItem('token');
    this.autenticado = false;
  }

  cargar(): void {
    this.clienteIdActivo = this.clienteIdEntrada;
  }
}
