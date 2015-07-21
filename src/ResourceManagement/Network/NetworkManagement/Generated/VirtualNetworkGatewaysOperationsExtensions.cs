namespace Microsoft.Azure.Management.Network
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Rest;
    using Microsoft.Azure;
    using Models;

    public static partial class VirtualNetworkGatewaysOperationsExtensions
    {
            /// <summary>
            /// The Put VirtualNetworkGateway operation creates/updates a virtual network
            /// gateway in the specified resource group through Network resource provider.
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='resourceGroupName'>
            /// The name of the resource group.
            /// </param>
            /// <param name='virtualNetworkGatewayName'>
            /// The name of the virtual network gateway.
            /// </param>
            /// <param name='parameters'>
            /// Parameters supplied to the Begin Create or update Virtual Network Gateway
            /// operation through Network resource provider.
            /// </param>
            public static VirtualNetworkGateway CreateOrUpdate(this IVirtualNetworkGatewaysOperations operations, string resourceGroupName, string virtualNetworkGatewayName, VirtualNetworkGateway parameters)
            {
                return Task.Factory.StartNew(s => ((IVirtualNetworkGatewaysOperations)s).CreateOrUpdateAsync(resourceGroupName, virtualNetworkGatewayName, parameters), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <summary>
            /// The Put VirtualNetworkGateway operation creates/updates a virtual network
            /// gateway in the specified resource group through Network resource provider.
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='resourceGroupName'>
            /// The name of the resource group.
            /// </param>
            /// <param name='virtualNetworkGatewayName'>
            /// The name of the virtual network gateway.
            /// </param>
            /// <param name='parameters'>
            /// Parameters supplied to the Begin Create or update Virtual Network Gateway
            /// operation through Network resource provider.
            /// </param>
            /// <param name='cancellationToken'>
            /// Cancellation token.
            /// </param>
            public static async Task<VirtualNetworkGateway> CreateOrUpdateAsync( this IVirtualNetworkGatewaysOperations operations, string resourceGroupName, string virtualNetworkGatewayName, VirtualNetworkGateway parameters, CancellationToken cancellationToken = default(CancellationToken))
            {
                AzureOperationResponse<VirtualNetworkGateway> result = await operations.CreateOrUpdateWithHttpMessagesAsync(resourceGroupName, virtualNetworkGatewayName, parameters, null, cancellationToken).ConfigureAwait(false);
                return result.Body;
            }

            /// <summary>
            /// The Put VirtualNetworkGateway operation creates/updates a virtual network
            /// gateway in the specified resource group through Network resource provider.
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='resourceGroupName'>
            /// The name of the resource group.
            /// </param>
            /// <param name='virtualNetworkGatewayName'>
            /// The name of the virtual network gateway.
            /// </param>
            /// <param name='parameters'>
            /// Parameters supplied to the Begin Create or update Virtual Network Gateway
            /// operation through Network resource provider.
            /// </param>
            public static VirtualNetworkGateway BeginCreateOrUpdate(this IVirtualNetworkGatewaysOperations operations, string resourceGroupName, string virtualNetworkGatewayName, VirtualNetworkGateway parameters)
            {
                return Task.Factory.StartNew(s => ((IVirtualNetworkGatewaysOperations)s).BeginCreateOrUpdateAsync(resourceGroupName, virtualNetworkGatewayName, parameters), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <summary>
            /// The Put VirtualNetworkGateway operation creates/updates a virtual network
            /// gateway in the specified resource group through Network resource provider.
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='resourceGroupName'>
            /// The name of the resource group.
            /// </param>
            /// <param name='virtualNetworkGatewayName'>
            /// The name of the virtual network gateway.
            /// </param>
            /// <param name='parameters'>
            /// Parameters supplied to the Begin Create or update Virtual Network Gateway
            /// operation through Network resource provider.
            /// </param>
            /// <param name='cancellationToken'>
            /// Cancellation token.
            /// </param>
            public static async Task<VirtualNetworkGateway> BeginCreateOrUpdateAsync( this IVirtualNetworkGatewaysOperations operations, string resourceGroupName, string virtualNetworkGatewayName, VirtualNetworkGateway parameters, CancellationToken cancellationToken = default(CancellationToken))
            {
                AzureOperationResponse<VirtualNetworkGateway> result = await operations.BeginCreateOrUpdateWithHttpMessagesAsync(resourceGroupName, virtualNetworkGatewayName, parameters, null, cancellationToken).ConfigureAwait(false);
                return result.Body;
            }

            /// <summary>
            /// The Get VirtualNetworkGateway operation retrieves information about the
            /// specified virtual network gateway through Network resource provider.
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='resourceGroupName'>
            /// The name of the resource group.
            /// </param>
            /// <param name='virtualNetworkGatewayName'>
            /// The name of the virtual network gateway.
            /// </param>
            public static VirtualNetworkGateway Get(this IVirtualNetworkGatewaysOperations operations, string resourceGroupName, string virtualNetworkGatewayName)
            {
                return Task.Factory.StartNew(s => ((IVirtualNetworkGatewaysOperations)s).GetAsync(resourceGroupName, virtualNetworkGatewayName), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <summary>
            /// The Get VirtualNetworkGateway operation retrieves information about the
            /// specified virtual network gateway through Network resource provider.
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='resourceGroupName'>
            /// The name of the resource group.
            /// </param>
            /// <param name='virtualNetworkGatewayName'>
            /// The name of the virtual network gateway.
            /// </param>
            /// <param name='cancellationToken'>
            /// Cancellation token.
            /// </param>
            public static async Task<VirtualNetworkGateway> GetAsync( this IVirtualNetworkGatewaysOperations operations, string resourceGroupName, string virtualNetworkGatewayName, CancellationToken cancellationToken = default(CancellationToken))
            {
                AzureOperationResponse<VirtualNetworkGateway> result = await operations.GetWithHttpMessagesAsync(resourceGroupName, virtualNetworkGatewayName, null, cancellationToken).ConfigureAwait(false);
                return result.Body;
            }

            /// <summary>
            /// The Delete VirtualNetworkGateway operation deletes the specifed virtual
            /// network Gateway through Network resource provider.
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='resourceGroupName'>
            /// The name of the resource group.
            /// </param>
            /// <param name='virtualNetworkGatewayName'>
            /// The name of the virtual network gateway.
            /// </param>
            public static void Delete(this IVirtualNetworkGatewaysOperations operations, string resourceGroupName, string virtualNetworkGatewayName)
            {
                Task.Factory.StartNew(s => ((IVirtualNetworkGatewaysOperations)s).DeleteAsync(resourceGroupName, virtualNetworkGatewayName), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <summary>
            /// The Delete VirtualNetworkGateway operation deletes the specifed virtual
            /// network Gateway through Network resource provider.
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='resourceGroupName'>
            /// The name of the resource group.
            /// </param>
            /// <param name='virtualNetworkGatewayName'>
            /// The name of the virtual network gateway.
            /// </param>
            /// <param name='cancellationToken'>
            /// Cancellation token.
            /// </param>
            public static async Task DeleteAsync( this IVirtualNetworkGatewaysOperations operations, string resourceGroupName, string virtualNetworkGatewayName, CancellationToken cancellationToken = default(CancellationToken))
            {
                await operations.DeleteWithHttpMessagesAsync(resourceGroupName, virtualNetworkGatewayName, null, cancellationToken).ConfigureAwait(false);
            }

            /// <summary>
            /// The Delete VirtualNetworkGateway operation deletes the specifed virtual
            /// network Gateway through Network resource provider.
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='resourceGroupName'>
            /// The name of the resource group.
            /// </param>
            /// <param name='virtualNetworkGatewayName'>
            /// The name of the virtual network gateway.
            /// </param>
            public static void BeginDelete(this IVirtualNetworkGatewaysOperations operations, string resourceGroupName, string virtualNetworkGatewayName)
            {
                Task.Factory.StartNew(s => ((IVirtualNetworkGatewaysOperations)s).BeginDeleteAsync(resourceGroupName, virtualNetworkGatewayName), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <summary>
            /// The Delete VirtualNetworkGateway operation deletes the specifed virtual
            /// network Gateway through Network resource provider.
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='resourceGroupName'>
            /// The name of the resource group.
            /// </param>
            /// <param name='virtualNetworkGatewayName'>
            /// The name of the virtual network gateway.
            /// </param>
            /// <param name='cancellationToken'>
            /// Cancellation token.
            /// </param>
            public static async Task BeginDeleteAsync( this IVirtualNetworkGatewaysOperations operations, string resourceGroupName, string virtualNetworkGatewayName, CancellationToken cancellationToken = default(CancellationToken))
            {
                await operations.BeginDeleteWithHttpMessagesAsync(resourceGroupName, virtualNetworkGatewayName, null, cancellationToken).ConfigureAwait(false);
            }

            /// <summary>
            /// The List VirtualNetworkGateways opertion retrieves all the virtual network
            /// gateways stored.
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='resourceGroupName'>
            /// The name of the resource group.
            /// </param>
            public static Page<VirtualNetworkGateway> List(this IVirtualNetworkGatewaysOperations operations, string resourceGroupName)
            {
                return Task.Factory.StartNew(s => ((IVirtualNetworkGatewaysOperations)s).ListAsync(resourceGroupName), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <summary>
            /// The List VirtualNetworkGateways opertion retrieves all the virtual network
            /// gateways stored.
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='resourceGroupName'>
            /// The name of the resource group.
            /// </param>
            /// <param name='cancellationToken'>
            /// Cancellation token.
            /// </param>
            public static async Task<Page<VirtualNetworkGateway>> ListAsync( this IVirtualNetworkGatewaysOperations operations, string resourceGroupName, CancellationToken cancellationToken = default(CancellationToken))
            {
                AzureOperationResponse<Page<VirtualNetworkGateway>> result = await operations.ListWithHttpMessagesAsync(resourceGroupName, null, cancellationToken).ConfigureAwait(false);
                return result.Body;
            }

            /// <summary>
            /// The Reset VirtualNetworkGateway operation resets the primary of the
            /// virtual network gatewayin the specified resource group through Network
            /// resource provider.
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='resourceGroupName'>
            /// The name of the resource group.
            /// </param>
            /// <param name='virtualNetworkGatewayName'>
            /// The name of the virtual network gateway.
            /// </param>
            /// <param name='parameters'>
            /// Parameters supplied to the Begin Reset Virtual Network Gateway operation
            /// through Network resource provider.
            /// </param>
            public static VirtualNetworkGateway Reset(this IVirtualNetworkGatewaysOperations operations, string resourceGroupName, string virtualNetworkGatewayName, VirtualNetworkGateway parameters)
            {
                return Task.Factory.StartNew(s => ((IVirtualNetworkGatewaysOperations)s).ResetAsync(resourceGroupName, virtualNetworkGatewayName, parameters), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <summary>
            /// The Reset VirtualNetworkGateway operation resets the primary of the
            /// virtual network gatewayin the specified resource group through Network
            /// resource provider.
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='resourceGroupName'>
            /// The name of the resource group.
            /// </param>
            /// <param name='virtualNetworkGatewayName'>
            /// The name of the virtual network gateway.
            /// </param>
            /// <param name='parameters'>
            /// Parameters supplied to the Begin Reset Virtual Network Gateway operation
            /// through Network resource provider.
            /// </param>
            /// <param name='cancellationToken'>
            /// Cancellation token.
            /// </param>
            public static async Task<VirtualNetworkGateway> ResetAsync( this IVirtualNetworkGatewaysOperations operations, string resourceGroupName, string virtualNetworkGatewayName, VirtualNetworkGateway parameters, CancellationToken cancellationToken = default(CancellationToken))
            {
                AzureOperationResponse<VirtualNetworkGateway> result = await operations.ResetWithHttpMessagesAsync(resourceGroupName, virtualNetworkGatewayName, parameters, null, cancellationToken).ConfigureAwait(false);
                return result.Body;
            }

            /// <summary>
            /// The Reset VirtualNetworkGateway operation resets the primary of the
            /// virtual network gatewayin the specified resource group through Network
            /// resource provider.
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='resourceGroupName'>
            /// The name of the resource group.
            /// </param>
            /// <param name='virtualNetworkGatewayName'>
            /// The name of the virtual network gateway.
            /// </param>
            /// <param name='parameters'>
            /// Parameters supplied to the Begin Reset Virtual Network Gateway operation
            /// through Network resource provider.
            /// </param>
            public static VirtualNetworkGateway BeginReset(this IVirtualNetworkGatewaysOperations operations, string resourceGroupName, string virtualNetworkGatewayName, VirtualNetworkGateway parameters)
            {
                return Task.Factory.StartNew(s => ((IVirtualNetworkGatewaysOperations)s).BeginResetAsync(resourceGroupName, virtualNetworkGatewayName, parameters), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <summary>
            /// The Reset VirtualNetworkGateway operation resets the primary of the
            /// virtual network gatewayin the specified resource group through Network
            /// resource provider.
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='resourceGroupName'>
            /// The name of the resource group.
            /// </param>
            /// <param name='virtualNetworkGatewayName'>
            /// The name of the virtual network gateway.
            /// </param>
            /// <param name='parameters'>
            /// Parameters supplied to the Begin Reset Virtual Network Gateway operation
            /// through Network resource provider.
            /// </param>
            /// <param name='cancellationToken'>
            /// Cancellation token.
            /// </param>
            public static async Task<VirtualNetworkGateway> BeginResetAsync( this IVirtualNetworkGatewaysOperations operations, string resourceGroupName, string virtualNetworkGatewayName, VirtualNetworkGateway parameters, CancellationToken cancellationToken = default(CancellationToken))
            {
                AzureOperationResponse<VirtualNetworkGateway> result = await operations.BeginResetWithHttpMessagesAsync(resourceGroupName, virtualNetworkGatewayName, parameters, null, cancellationToken).ConfigureAwait(false);
                return result.Body;
            }

            /// <summary>
            /// The List VirtualNetworkGateways opertion retrieves all the virtual network
            /// gateways stored.
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='nextPageLink'>
            /// NextLink from the previous successful call to List operation.
            /// </param>
            public static Page<VirtualNetworkGateway> ListNext(this IVirtualNetworkGatewaysOperations operations, string nextPageLink)
            {
                return Task.Factory.StartNew(s => ((IVirtualNetworkGatewaysOperations)s).ListNextAsync(nextPageLink), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <summary>
            /// The List VirtualNetworkGateways opertion retrieves all the virtual network
            /// gateways stored.
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='nextPageLink'>
            /// NextLink from the previous successful call to List operation.
            /// </param>
            /// <param name='cancellationToken'>
            /// Cancellation token.
            /// </param>
            public static async Task<Page<VirtualNetworkGateway>> ListNextAsync( this IVirtualNetworkGatewaysOperations operations, string nextPageLink, CancellationToken cancellationToken = default(CancellationToken))
            {
                AzureOperationResponse<Page<VirtualNetworkGateway>> result = await operations.ListNextWithHttpMessagesAsync(nextPageLink, null, cancellationToken).ConfigureAwait(false);
                return result.Body;
            }

    }
}
