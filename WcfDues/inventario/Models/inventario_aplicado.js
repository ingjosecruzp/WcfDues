app.factory('inventario_aplicado', function ($resource,resourceInterceptor,Variables) {
	return $resource('http://'+ Variables.IpServidor+'/WSInventarioAplicado.svc/inventariosaplicados/:item',{item: "@item"},
										{
											'get':    {method:'GET',interceptor: resourceInterceptor},
											'save':   {method:'POST',interceptor: resourceInterceptor},
											'query':  {method:'GET', isArray:true,interceptor: resourceInterceptor,url:'http://'+ Variables.IpServidor+'/WSInventarioAplicado.svc/inventarios?method=:method'},
											'remove': {method:'DELETE',interceptor: resourceInterceptor},
											'delete': {method:'DELETE',interceptor: resourceInterceptor}
										}
									);
});