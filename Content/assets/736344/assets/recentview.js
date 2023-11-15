//Recent product
var cookieList = function(cookieName) {
	//When the cookie is saved the items will be a comma seperated string
	//So we will split the cookie by comma to get the original array
	var cookie = $.cookie(cookieName);
	//Load the items or a new array if null.
	var items = cookie ? cookie.split(/,/) : new Array();
	return {
		"add": function(val) {

			var i;
			var check = false;
			for (i = 0; i< items.length;i++){
				if(val == items[i]){
					check= true;
				}
			}
			if(check == false){
				items.push(val);
			}
			//Save the items to a cookie.
			//EDIT: Modified from linked answer by Nick see
			//      http://stackoverflow.com/questions/3387251/how-to-store-array-in-jquery-cookie
			$.cookie(cookieName, items.join(','));
		},
		"remove": function (index) {
			//EDIT: Thx to Assef and luke for remove.
			items.splice(index, 1);
			$.cookie(cookieName, items.join(','));        },
		"clear": function() {
			items = null;
			//clear the cookie.
			$.cookie(cookieName, null);
		},
		"items": function() {

			return items;
		}
	}
}

function getProduct(alias) {


	Bizweb.getProduct(alias,function(product){
	//	console.log(product);

		var rv ="";
		

		
		var pt ="";
		 if (product.price < 1){
			 var price = '<span class="special-price"><span class="price product-price">Liên hệ </span></span> <!-- Hết hàng -->';
		 }else{
			 if(product.variants[0].compare_at_price && product.variants[0].compare_at_price > product.variants[0].price){
				 var pt = ((product.variants[0].compare_at_price - product.variants[0].price))/product.variants[0].compare_at_price * 100;				
				 pt = '';
			 }
			 var price = '<div class="special-price"><span class="price product-price">'+  Bizweb.formatMoney(product.price_min, "{{amount_no_decimals_with_comma_separator}}₫" ) +'</span> </div>';

		 }
		 if(product.summary != null && product.summary !=""){
			 var productDes = product.summary;
		 }else{
			 if(product.content != null){
				 var productDes = product.content.replace(/(<([^>]+)>)/ig,"");
				 productDes = productDes.slice(0,180);
			 }else{
				 var productDes = "";
			 }
		 }


		 
		 if(product.images.length == 0){
			 var img = '<img src="//bizweb.dktcdn.net/thumb/medium/assets/themes_support/noimage.gif" alt="'+product.name+'">'
			 }else{
				 var img = '<img src="'+product.featured_image+'" alt="'+product.name+'">'
				 }
		  $('<div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">'
			+'<div class="product_border">'
			+'<div class="product-loop-1 product-loop-2 product-base">'
			+'<div class="product-thumbnail flexbox-grid relative">'
			+'<a class="image_link display_flex" title="'+product.name+'" href="'+product.url+'">'
			+ img
			+'</a>'
			+ pt
			+'<div class="price-box margin-top-0"><div class="price">'
			

			+'</div></div>'
			+'</div>'
			+'<div class="product-info a-left">'
			+'<h3 class="product-name"><a href="'+product.url+'" title="'+product.name+'">'+product.name+'</a></h3>'
			+'<div class="relative hidden">'
			
			+price
			+'</div>'
			+'</div>'
			+'</div>'
			+'</div></div>').appendTo('#recent-content');
		 })
		}
		var list = new cookieList("MyItems");
		list.add(alias);
		//list.items();
		//list.remove(0);
		var i;
		var limit = list.items().length;
		if(limit > getLimit){
			var fmit = limit - getLimit;

			var r;
			for(r = 0;r < fmit; r++ ){

				list.remove(r);
			}
		}
		if(navigator.userAgent.indexOf("Speed Insights") == -1) {
			for(i = limit-1;i >= 0; i-- ){
				getProduct(list.items()[i]);
				if(i == 0){

				}

			}
		}