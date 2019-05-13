$(() => {
    $(".btn-like").on('click', function () {
        console.log("hi");
        const jokeId = $(this).data('id');
        console.log(jokeId);
        const like = true;
        console.log(like);
        $.post("/home/likeJoke", { jokeId, like }, function (response) {
            if (response.result == 'Redirect')
                window.location = response.url;
            $(`#like-${jokeId}`).attr('disabled', true);
            $.get("/home/getlikes", { jokeId }, function (like) {
                $(`#likes-${jokeId}`).text(like);
            });
        });
    });

    $(".btn-dislike").on('click', function () {
        const jokeId = $(this).data('id');
        $.post("/home/likeJoke", { jokeId, like: false }, function (response) {
            if (response.result == 'Redirect')
                window.location = response.url;
            $(`#dislike-${jokeId}`).attr('disabled', true);
            $.get("/home/getlikes", { jokeId }, function (like) {
                $(`#likes-${jokeId}`).text(like);
            });
        });
    });



});