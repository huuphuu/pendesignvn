/*
Copyright (c) 2003-2013, CKSource - Frederico Knabben. All rights reserved.
For licensing, see LICENSE.html or http://ckeditor.com/license
*/

CKEDITOR.editorConfig = function (config) {
    // ALLOW <i></i>
    config.protectedSource.push(/<i[^>]*><\/i>/g);
    config.language = 'vi';
    config.extraPlugins = 'confighelper';
    // Define changes to default configuration here. For example:
    // config.language = 'fr';
    // config.uiColor = '#AADC6E';
    //config.toolbarGroups_HomeIntro = [
    //	{ name: 'clipboard', groups: ['clipboard', 'undo'] },
    //	{ name: 'editing', groups: ['find', 'selection', 'spellchecker', 'editing'] },
    //	{ name: 'links', groups: ['links'] },
    //	{ name: 'insert', groups: ['insert'] },
    //	{ name: 'forms', groups: ['forms'] },
    //	{ name: 'tools', groups: ['tools'] },
    //	{ name: 'document', groups: ['mode', 'document', 'doctools'] },
    //	{ name: 'others', groups: ['others'] },
    //	'/',
    //	{ name: 'basicstyles', groups: ['basicstyles', 'cleanup'] },
    //	{ name: 'paragraph', groups: ['list', 'indent', 'blocks', 'align', 'bidi', 'paragraph'] },
    //	{ name: 'styles', groups: ['styles'] },
    //	{ name: 'colors', groups: ['colors'] },
    //	{ name: 'about', groups: ['about'] }
    //];
    config.toolbar_Full = [
        ['Source', 'Save', 'NewPage', 'Preview'],
        ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-', 'Print', 'SpellChecker', 'Scayt'],

        ['Image', 'Link', 'Unlink', 'Anchor', '-', 'Table', 'HorizontalRule', 'Smiley', 'SpecialChar', 'PageBreak', 'SelectAll', 'RemoveFormat', 'Undo', 'Redo', '-', 'Find', 'Replace'],
                '/',
        ['Bold', 'Italic', 'Underline', 'Strike', '-', 'Subscript', 'Superscript', 'NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', 'Blockquote', 'CreateDiv'],
        ['JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'],
        '/',
        ['Styles', 'Format', 'Font', 'FontSize'],
        ['TextColor', 'BGColor'],
        ['ShowBlocks']
    ];
    config.skin = 'kama';
};
