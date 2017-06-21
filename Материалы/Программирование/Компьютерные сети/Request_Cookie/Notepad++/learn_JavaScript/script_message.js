function dev() {
		alert('Hello');
		return;
	}

function sistemi_schisl(){
		var num_int = 123;
		alert("Число " + num_int + " = " + num_int.toString(2));
		var num_dv = "1111111";
		alert("Двоичное число " + num_dv + " = " + parseInt(num_dv, 2))
		return;
	}
	
function shiphr_data(){
		var dat = document.getElementById("Data");
		var key1 = document.getElementById("Key");
		var rez1 = document.getElementById("Rez1");
		rez1.value = dat.value ^ key1.value;
		return;
	}
	
function unshiphr_data(){
		var dat = document.getElementById("Data");
		var key1 = document.getElementById("Key");
		var rez2 = document.getElementById("Rez2");
		rez2.value = dat.value ^ key1.value ^ key1.value;
		return;
	}