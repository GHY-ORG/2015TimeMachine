$(function () {

    $close = $("#upload-pic-form-close");
    $form = $("#upload-pic-form");


    $btn_pic_add = $("#btn-pic-add");
    $btn_pic_info = $("#btn-pic-info");
    $btn_story_add = $("#btn-story-add");
    $btn_story_info = $("#btn-story-info");

    $type = $form.find("#form-type");
    $order = $form.find("#form-order");


    $close.click(function () {
        $form.get(0).reset();
        $form.fadeOut();
    });
    $btn_pic_add.click(function () {
        $type.val(1); $order.val(0);
        $form.fadeIn();
    });
});