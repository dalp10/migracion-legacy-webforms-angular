import { Component, Input, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PedidoService } from '../services/pedido.service';

@Component({
  selector: 'app-pedidos-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './pedidos-list.component.html'
})
export class PedidosListComponent implements OnInit {
  @Input() clienteId!: number;
  pedidos$ = this.pedidoService.pedidos$;

  constructor(private pedidoService: PedidoService) {}

  ngOnInit(): void {
    this.pedidoService.cargarPorCliente(this.clienteId);
  }

  aprobar(pedidoId: number): void {
    this.pedidoService.aprobar(pedidoId);
  }
}
