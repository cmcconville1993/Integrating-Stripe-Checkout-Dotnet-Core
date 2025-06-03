// let stripe;
// let elements;
// let card;
// let clientSecret;

// window.initializeStripe = function (secret) {
//     try {
//         clientSecret = secret;
//         stripe = Stripe('pk_test_51R5VHyG3aOdvMyTs3Gs04LjsA3hL7WIDA6AFQt0lJqdzyZOIPnmALNq92TcuL0LXZOzUaG2PqE9YK2a4LnyHNGH400CwZvdaHA');
//         elements = stripe.elements();

//         // Create card Element
//         card = elements.create('card', {
//             style: {
//                 base: {
//                     color: '#32325d',
//                     fontFamily: '"Helvetica Neue", Helvetica, sans-serif',
//                     fontSmoothing: 'antialiased',
//                     fontSize: '16px',
//                     '::placeholder': {
//                         color: '#aab7c4'
//                     }
//                 },
//                 invalid: {
//                     color: '#dc2626',
//                     iconColor: '#dc2626'
//                 }
//             }
//         });

//         // Mount the card Element
//         card.mount('#card-element');

//         // Handle validation errors
//         card.addEventListener('change', function(event) {
//             var displayError = document.getElementById('card-errors');
//             if (event.error) {
//                 displayError.textContent = event.error.message;
//             } else {
//                 displayError.textContent = '';
//             }
//         });

//         return true;
//     } catch (error) {
//         console.error('Stripe initialization error:', error);
//         return false;
//     }
// };

// window.confirmPayment = async function () {
//     try {
//         if (!stripe || !card || !clientSecret) {
//             throw new Error('Stripe not properly initialized');
//         }

//         const result = await stripe.confirmCardPayment(clientSecret, {
//             payment_method: {
//                 card: card,
//             }
//         });

//         if (result.error) {
//             return JSON.stringify({ error: result.error.message });
//         } else {
//             return JSON.stringify({ 
//                 paymentIntentId: result.paymentIntent.id,
//                 error: null 
//             });
//         }
//     } catch (e) {
//         console.error('Payment error:', e);
//         return JSON.stringify({ error: e.message });
//     }
// }; 