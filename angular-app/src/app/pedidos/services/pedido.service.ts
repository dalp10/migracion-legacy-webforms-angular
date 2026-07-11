import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject } from 'rxjs';
import { Pedido } from '../../shared/models/pedido.model';

@Injectable({ providedIn: 'root' })
export class PedidoService {
  private apiUrl = '/api/pedidos';
  private pedidosSubject = new BehaviorSubject<Pedido[]>([]);
  pedidos$ = this.pedidosSubject.asObservable();

  constructor(private http: HttpClient) {}

  cargarPorCliente(clienteId: number): void {
    this.http.get<Pedido[]>(`${this.apiUrl}?clienteId=${clienteId}`)
      .subscribe(pedidos => this.pedidosSubject.next(pedidos));
  }

  aprobar(pedidoId: number): void {
    this.http.post<Pedido>(`${this.apiUrl}/${pedidoId}/aprobar`, {}).subscribe(actualizado => {
      const actuales = this.pedidosSubject.value;
      const index = actuales.findIndex(p => p.id === pedidoId);
      const nuevos = [...actuales];
      nuevos[index] = actualizado;
      this.pedidosSubject.next(nuevos);
    });
  }
}
