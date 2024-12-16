$(document).ready(function () {
		setTimeout(function () {
			$('#exampleModal').modal({
				backdrop: 'static',  // جلوگیری از تار شدن پس‌زمینه
				keyboard: false       // جلوگیری از بسته شدن مودال با کلید ESC
			}).modal('show');

			setTimeout(function () {
				$('#exampleModal').modal('hide');
			}, 7000);
		}, 50);
	});